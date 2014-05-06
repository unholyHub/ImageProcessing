// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Filtration.cs" company="ZhZ Inc.">
//   Copyright (c) ZhZ Inc. All rights reserved.
// </copyright>
// <summary>
//   Defines the Filtration class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ImageProcessingTechniques
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The filtration class for an image processing.
    /// </summary>
    public static class Filtration
    {
        /// <summary>
        /// The lock bits filtration technique for image processing.
        /// </summary>
        /// <param name="sourceImage">
        /// The source image.
        /// </param>
        /// <param name="value">
        /// The value for comparison.
        /// </param>
        /// <returns>
        /// The <see cref="Bitmap"/> that is filtered.
        /// </returns>
        public static Bitmap LockBitsFiltration(Image sourceImage, byte value)
        {
            Bitmap bmp = sourceImage as Bitmap;

            if (bmp == null)
            {
                return new Bitmap(sourceImage);
            }

            // Lock the bitmap's bits.
            BitmapData bmpData = bmp.LockBits(
                new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            byte[] byteBuffer = new byte[bmpData.Stride * bmp.Height];

            // Copy the RGB values into the array.
            Marshal.Copy(ptr, byteBuffer, 0, byteBuffer.Length);

            for (int i = 0; i < byteBuffer.Length; i++)
            {
                if (byteBuffer[i] <= value)
                {
                    byteBuffer[i] = 0;
                }
                else
                {
                    byteBuffer[i] = 255;
                }
            }

            // Copy the RGB values back to the bitmap
            Marshal.Copy(byteBuffer, 0, ptr, byteBuffer.Length);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);

            return bmp;
        }

        /// <summary>
        /// The get set pixel filtration technique for image processing.
        /// </summary>
        /// <param name="sourceImage">
        /// The source image.
        /// </param>
        /// <param name="value">
        /// The value for comparison.
        /// </param>
        /// <returns>
        /// The <see cref="Bitmap"/> that is filtered.
        /// </returns>
        public static Bitmap GetSetPixelFiltration(Image sourceImage, byte value)
        {
            Bitmap bmp = sourceImage as Bitmap;

            if (bmp == null)
            {
                return new Bitmap(sourceImage.Width, sourceImage.Height);
            }

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color pixel = bmp.GetPixel(x, y);

                    if (pixel.R < value || pixel.B < value || pixel.G < value)
                    {
                        bmp.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        bmp.SetPixel(x, y, Color.White);
                    }
                }
            }

            return bmp;
        }
    }
}
