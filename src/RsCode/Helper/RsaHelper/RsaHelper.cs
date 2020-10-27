﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Reflection;

namespace Rswl.License.Web.Models.RsaHelper
{
    /*
     * 文档
生成密钥
使用“RsaKeyGenerator”类。返回的结果是一个有两个元素的字符串的列表，元素1是私钥，元素2是公钥。

格式：XML

var keyList = RsaKeyGenerator.XmlKey（2048）;
var privateKey = keyList [0];
var publicKey = keyList [1];
格式：Pkcs1

var keyList = RsaKeyGenerator.Pkcs1Key（2048）;
var privateKey = keyList [0];
var publicKey = keyList [1];
格式：Pkcs8

var keyList = RsaKeyGenerator.Pkcs8Key（2048）;
var privateKey = keyList [0];
var publicKey = keyList [1];
RSA密钥转换
使用“RsaKeyConvert”类。它支持这三种格式的密钥转换，即：xml，pkcs1，pkcs8。

XML-> Pkcs1：
私钥：RsaKeyConvert.PrivateKeyXmlToPkcs1（）
公钥：RsaKeyConvert.PublicKeyXmlToPem（）
XML-> Pkcs8：
私钥：RsaKeyConvert.PrivateKeyXmlToPkcs8（）
公钥：RsaKeyConvert.PublicKeyXmlToPem（）
Pkcs1-> XML：
私钥：RsaKeyConvert.PrivateKeyPkcs1ToXml（）
公钥：RsaKeyConvert.PublicKeyPemToXml（）
Pkcs1-> Pkcs8：
私钥：RsaKeyConvert.PrivateKeyPkcs1ToPkcs8（）
公钥：不需要转换
Pkcs8-> XML：
私钥：RsaKeyConvert.PrivateKeyPkcs8ToXml（）
公钥：RsaKeyConvert.PublicKeyPemToXml（）
Pkcs8-> Pkcs1：
私钥：RsaKeyConvert.PrivateKeyPkcs8ToPkcs1（）
公钥：不需要转换
加密，解密，签名和验证签名
XML，Pkcs1，Pkcs8分别对应类：RsaXmlUtil，RsaPkcs1Util，RsaPkcs8Util。它们继承自抽象类RSAUtilBase

加密：RSAUtilBase.Encrypt（）
解密：RSAUtilBase.Decrypt（）
Sign：RSAUtilBase.SignData（）
验证：RSAUtilBase.VerifyData（）
PEM格式化
使用类“RsaPemFormatHelper”。

格式化Pkcs1格式私钥：RsaPemFormatHelper.Pkcs1PrivateKeyFormat（）
删除Pkcs1格式私钥格式：RsaPemFormatHelper.Pkcs1PrivateKeyFormatRemove（）
格式化Pkcs8格式私钥：RsaPemFormatHelper.Pkcs8PrivateKeyFormat（）
删除Pkcs8格式的私钥格式：RsaPemFormatHelper.Pkcs8PrivateKeyFormatRemove（）
     */

    //public class ttt
    //{
    //    public void GetAssemblyPublicKey()
    //    {
    //        Assembly assembly = Assembly.GetExecutingAssembly();
    //        byte[] pKey = assembly.GetName().GetPublicKey();
    //        byte[] pKeyToken = assembly.GetName().GetPublicKeyToken();
    //        //公钥 GetString(pKey)
            
    //    }

    //    static string GetString(byte[] bytes)
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        foreach (var item in bytes)
    //        {
    //            sb.Append(string.Format("{0:x}",item));
    //        }
    //        return sb.ToString();
    //    }
    //}
}
