using System;
using System.Drawing;
using System.Windows.Forms;

class Program
{
    static void Main()
    {
        string[] imagePaths = { "image1.jpg", "image2.jpg", "image3.jpg" };

        // Делегат типу Func<Bitmap, Bitmap> для операції обробки зображення
        Func<Bitmap, Bitmap> imageProcessingOperation = (bitmap) =>
        {
            // Ваша операція обробки зображення
            return GrayscaleFilter(bitmap);
        };

        // Дія для відображення обробленого зображення
        Action<Bitmap> displayImage = (bitmap) =>
        {
            bitmap.ShowImage();
        };

        foreach (var imagePath in imagePaths)
        {
            Bitmap originalImage = new Bitmap(imagePath);

            // Виконання операції обробки зображення
            Bitmap processedImage = imageProcessingOperation(originalImage);

            // Відображення обробленого зображення
            displayImage(processedImage);
        }
    }

    // Приклад операції обробки зображення - перетворення у відтінки сірого
    static Bitmap GrayscaleFilter(Bitmap bitmap)
    {
        Bitmap grayscaleImage = new Bitmap(bitmap.Width, bitmap.Height);

        for (int i = 0; i < bitmap.Width; i++)
        {
            for (int j = 0; j < bitmap.Height; j++)
            {
                Color pixel = bitmap.GetPixel(i, j);
                int average = (pixel.R + pixel.G + pixel.B) / 3;
                Color grayscalePixel = Color.FromArgb(average, average, average);
                grayscaleImage.SetPixel(i, j, grayscalePixel);
            }
        }

        return grayscaleImage;
    }
}

static class ImageExtensions
{
    // Розширення для відображення зображення
    public static void ShowImage(this Bitmap bitmap)
    {
        using (var form = new Form())
        {
            form.ClientSize = bitmap.Size;
            form.BackgroundImage = bitmap;
            form.BackgroundImageLayout = ImageLayout.Zoom;
            form.ShowDialog();
        }
    }
}

