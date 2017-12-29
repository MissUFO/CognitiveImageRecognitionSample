using System;
using System.Collections.Generic;
using ImgRecognition.BusinessLogic.DataObjects.Interface;

namespace ImgRecognition.BusinessLogic.DataObjects
{
    /// <summary>
    /// Basic object after recognition
    /// </summary>
    public class ImgDescription : IImgDescription
    {
        public string Description { get; set; }

        public List<string> Tags { get; set; }

        public List<string> Errors { get; set; }

    }
}
