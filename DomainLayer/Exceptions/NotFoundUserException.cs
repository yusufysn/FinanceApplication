﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException() : base("Kullanıcı Adı veya şifre hatalı.")
        {
        }

        public NotFoundUserException(string? message) : base(message)
        {
        }

        public NotFoundUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
