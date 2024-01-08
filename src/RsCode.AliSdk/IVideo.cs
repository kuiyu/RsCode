using AlibabaCloud.SDK.Videoenhan20200320.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsCode.AliSdk
{
    public interface IVideo
    {
        /// <summary>
        /// 视频人像卡通化
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GenerateHumanAnimeStyleVideoResponse GenerateHumanAnimeStyleVideo(GenerateHumanAnimeStyleVideoRequest request);
        /// <summary>
        /// 视频人脸融合能力可以将视频中检测到的最大人脸，融合进另一个人的人脸特征，达到换脸的感官效果
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MergeVideoFaceResponse MergeVideoFace(MergeVideoFaceRequest request);
        /// <summary>
        /// 视频人脸融合MergeVideoFace
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MergeVideoModelFaceResponse MergeVideoModelFace(MergeVideoModelFaceRequest request);
        GetAsyncJobResultResponse GetJobResult(string jobId);
    }
}
