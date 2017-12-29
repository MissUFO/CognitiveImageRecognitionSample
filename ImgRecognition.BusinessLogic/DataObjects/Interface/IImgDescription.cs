using System.Collections.Generic;

namespace ImgRecognition.BusinessLogic.DataObjects.Interface
{
    /// <summary>
    /// Basic interface to return after recognition
    /// </summary>
    public interface IImgDescription
    {
        string Description { get; set; }

        List<string> Tags { get; set; }

        List<string> Errors { get; set; }
    }
}
