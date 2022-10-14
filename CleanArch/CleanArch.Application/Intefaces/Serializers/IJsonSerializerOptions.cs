﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CleanArch.Application.Intefaces.Serializers
{
    public interface IJsonSerializerOptions
    {
        /// <summary>
        /// Options for <see cref="System.Text.Json"/>.
        /// </summary>
        public JsonSerializerOptions JsonSerializerOptions { get; }
    }
}
