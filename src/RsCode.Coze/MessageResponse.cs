using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RsCode.Coze
{
    /// <summary>
    ///     Represents a message within a thread.
    /// </summary>
    public record MessageResponse : BaseResponse
    {
        [JsonPropertyName("delta")]
        public MessageResponse Delta
        {
            set => Content = value.Content;
        }

        /// <summary>
        ///     The thread ID that this message belongs to.
        /// </summary>
        [JsonPropertyName("thread_id")]
        public string ThreadId { get; set; }

        /// <summary>
        ///     The entity that produced the message. One of user or assistant.
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; }

        /// <summary>
        ///     The content of the message in array of text and/or images.
        /// </summary>
        [JsonPropertyName("content")]
        public List<MessageContentResponse>? Content { get; set; }

        /// <summary>
        ///     The status of the message, which can be either in_progress, incomplete, or completed.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }


        /// <summary>
        ///     On an incomplete message, details about why the message is incomplete.
        /// </summary>
        [JsonPropertyName("incomplete_details")]
        public IncompleteDetails? IncompleteDetails { get; set; }

        /// <summary>
        ///     The Unix timestamp (in seconds) for when the run was completed.
        /// </summary>
        [JsonPropertyName("completed_at")]
        public int? CompletedAt { get; set; }

        /// <summary>
        ///     The Unix timestamp (in seconds) for when the run was completed.
        /// </summary>
        [JsonPropertyName("incomplete_at")]
        public int? IncompleteAt { get; set; }

        /// <summary>
        ///     The ID of the run associated with the creation of this message. Value is null when messages are created manually
        ///     using the create message or create thread endpoints.
        /// </summary>
        [JsonPropertyName("run_id")]
        public string? RunId { get; set; }

        /// <summary>
        ///     A list of file IDs that the assistant should use.
        ///     Useful for tools like retrieval and code_interpreter that can access files.
        ///     A maximum of 10 files can be attached to a message.
        /// </summary>
        [JsonPropertyName("attachments")]
        public List<Attachment> Attachments { get; set; }

        /// <summary>
        ///     If applicable, the ID of the assistant that authored this message.
        /// </summary>
        [JsonPropertyName("assistant_id")]
        public string? AssistantId { get; set; }

        /// <summary>
        ///     The Unix timestamp (in seconds) for when the message was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public long CreatedAtUnix { get; set; }
        /// <summary>
        ///    for when the message was created.
        /// </summary>
        public DateTimeOffset CreatedAt => DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnix);

        /// <summary>
        ///     The identifier, which can be referenced in API endpoints.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        ///     Set of 16 key-value pairs that can be attached to an object.
        /// </summary>
        [JsonPropertyName("metadata")]
        public Dictionary<string, string>? Metadata { get; set; }


        /// <summary>
        ///     The content of the message:  text and/or images.
        /// </summary>
        public record MessageContentResponse
        {
            /// <summary>
            ///     text and/or images. image_file text
            /// </summary>
            [JsonPropertyName("type")]
            public string Type { get; set; }

            /// <summary>
            ///     References an image File in the content of a message.
            /// </summary>
            [JsonPropertyName("image_file")]
            public MessageImageFile? ImageFile { get; set; }

            /// <summary>
            ///     The text content that is part of a message.
            /// </summary>
            [JsonPropertyName("text")]
            public MessageText? Text { get; set; }

            /// <summary>
            ///     References an image URL in the content of a message.
            /// </summary>
            [JsonPropertyName("image_url")]
            public MessageImageUrl? ImageUrl { get; set; }
        }
        /// <summary>
        ///     File citation |File path
        /// </summary>
        public record MessageAnnotation
        {
            /// <summary>
            ///     type can be：file_citation、file_path
            /// </summary>
            [JsonPropertyName("type")]
            public string Type { get; set; }

            /// <summary>
            ///     The text in the message content that needs to be replaced.
            /// </summary>
            [JsonPropertyName("text")]
            public string Text { get; set; }

            [JsonPropertyName("start_index")]
            public int StartIndex { get; set; }

            [JsonPropertyName("end_index")]
            public int EndIndex { get; set; }

            [JsonPropertyName("file_citation")]
            public FileCitation FileCitation { get; set; }
        }

        public record FileCitation
        {
            /// <summary>
            ///     The ID of the specific File the citation/content  is from.
            /// </summary>
            [JsonPropertyName("file_id")]
            public string FileId { get; set; }

            /// <summary>
            ///     The specific quote in the file.
            /// </summary>
            [JsonPropertyName("quote")]
            public string Quote { get; set; }
        }
        /// <summary>
        ///     The text content that is part of a message.
        /// </summary>
        public record MessageText
        {
            /// <summary>
            ///     The data that makes up the text.
            /// </summary>
            [JsonPropertyName("value")]
            public string Value { get; set; }

            /// <summary>
            ///     annotations
            /// </summary>
            [JsonPropertyName("annotations")]
            public List<MessageAnnotation> Annotations { get; set; }
        }

        /// <summary>
        ///     The image_url object of vision message content
        /// </summary>
        public class MessageImageUrl
        {
            /// <summary>
            ///     The Url property
            ///     Images are made available to the model in two main ways: by passing a link to the image or by passing the base64
            ///     encoded image directly in the url property.
            ///     link example: "url" :
            ///     "https://upload.wikimedia.org/wikipedia/commons/thumb/d/dd/Gfp-wisconsin-madison-the-nature-boardwalk.jpg/2560px-Gfp-wisconsin-madison-the-nature-boardwalk.jpg"
            ///     base64 encoded image example: "url" : "data:image/jpeg;base64,{base64_image}"
            ///     Limitations:
            ///     OpenAI currently supports PNG (.png), JPEG (.jpeg and .jpg), WEBP (.webp), and non-animated GIF (.gif) image
            ///     formats
            ///     Image upload size is limited to 20MB per image
            ///     Captcha submission is blocked
            /// </summary>
            [JsonPropertyName("url")]
            public string Url { get; set; }

            /// <summary>
            ///     The optional Detail property controls low or high fidelity image understanding
            ///     It has three options, low, high, or auto, you have control over how the model processes the image and generates its
            ///     textual understanding.
            ///     By default, the model will use the auto setting which will look at the image input size and decide if it should use
            ///     the low or high setting.
            ///     low will disable the “high res” model. The model will receive a low-res 512px x 512px version of the image.
            ///     high will enable “high res” mode, which first allows the model to see the low res image and then creates detailed
            ///     crops of input images
            ///     as 512px squares based on the input image size.
            /// </summary>
            [JsonPropertyName("detail")]
            public string? Detail { get; set; }
        }

        public class MessageImageFile
        {
            /// <summary>
            ///     The File ID of the image in the message content. Set purpose="vision" when uploading the File if you need to later
            ///     display the file content.
            /// </summary>
            [JsonPropertyName("file_id")]
            public string FileId { get; set; }

            /// <summary>
            ///     Specifies the detail level of the image if specified by the user. low uses fewer tokens, you can opt in to high
            ///     resolution using high.
            /// </summary>
            [JsonPropertyName("detail")]
            public string Detail { get; set; }
        }
    }
}
