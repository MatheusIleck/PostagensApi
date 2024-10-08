﻿using System.ComponentModel.DataAnnotations;

namespace PostagensApi.Requests.Post
{
    public class DeletePostRequest : Request
    {
        [Required(ErrorMessage = "A value is required")]
        public long Id { get; set; }
    }
}
