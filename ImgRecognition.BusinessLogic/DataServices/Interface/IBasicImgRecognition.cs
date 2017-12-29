using System.IO;
using System.Threading.Tasks;

namespace ImgRecognition.BusinessLogic.DataServices.Interface
{
    /// <summary>
    /// Basic interface for img recognition
    /// </summary>
    public interface IBasicImgRecognition<A>
    {
        Task<A> RecognizeImage(Stream photo);
    }
}
