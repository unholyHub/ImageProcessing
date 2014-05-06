// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Crop.cs" company="ZhZ Inc.">
//   Copyright (c) ZhZ Inc. All rights reserved.
// </copyright>
// <author>Zhivko Kabaivanov</author>
// <summary> Defines the Crop class for image cropping. </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ImageProcessingTechniques
{
    using System.Drawing;
    using System.Drawing.Imaging;

    /// <summary>
    /// The crop class for image cropping.
    /// </summary>
    public static class Crop
    {
        /// <summary>
        /// Cropping using clone method for cropping.
        /// </summary>
        /// <param name="currentBitmap">
        /// The current bitmap for cropping.
        /// </param>
        /// <param name="xPosition">
        /// The x position of the start for the cropping.
        /// </param>
        /// <param name="yPosition">
        /// The y position of the start for the cropping.
        /// </param>
        /// <param name="width">
        /// The width for cropping.
        /// </param>
        /// <param name="height">
        /// The height for cropping.
        /// </param>
        /// <returns>
        /// The <see cref="Image"/> that is cropped with the given parameters.
        /// </returns>
        public static Image CloneCrop(Bitmap currentBitmap, int xPosition, int yPosition, int width, int height)
        {
            if (xPosition + width > currentBitmap.Width)
            {
                width = currentBitmap.Width - xPosition;
            }

            if (yPosition + height > currentBitmap.Height)
            {
                height = currentBitmap.Height - yPosition;
            }

            currentBitmap = currentBitmap.Clone(
                new Rectangle(xPosition, yPosition, width, height),
                PixelFormat.Format32bppPArgb);

            return currentBitmap;
        }

        /// <summary>
        /// Cropping method using a rectangle manipulation.
        /// </summary>
        /// <param name="bmp">
        /// Input bitmap image for processing.
        /// </param>
        /// <param name="startX">
        /// The x position of the start for the cropping.
        /// </param>
        /// <param name="startY">
        /// The y position of the start for the cropping.
        /// </param>
        /// <param name="width">
        /// The width for cropping.
        /// </param>
        /// <param name="height">
        /// The height for cropping.
        /// </param>
        /// <returns>
        /// The <see cref="Image"/> that is cropped with the given parameters.
        /// </returns>
        public static Image RectangleCrop(Bitmap bmp, int startX, int startY, int width, int height)
        {
            Rectangle srcRect = new Rectangle(startX, startY, width, height);

            Bitmap dest = new Bitmap(srcRect.Width, srcRect.Height);

            Rectangle destRect = new Rectangle(0, 0, srcRect.Width, srcRect.Height);

            Graphics graphics = Graphics.FromImage(dest);

            graphics.DrawImage(bmp, destRect, srcRect, GraphicsUnit.Pixel);

            return dest;
        }
    }
}
