﻿using System;

namespace RPoney.Log
{
    internal class TopLogMessage
    {
        // Methods
        public TopLogMessage()
        {
        }

        public TopLogMessage(string bizType, string description)
        {
            BizType = bizType;
            Description = description;
        }

        public TopLogMessage(string bizType, string description, string className)
        {
            BizType = bizType;
            Description = description;
            ClassName = className;
        }

        // Properties
        public string BizType { get; set; }

        public string ClassName { get; set; }

        public string Description { get; set; }

        public string EventNo { get; set; }

        public TopContext TopContext { get; set; }

        public override string ToString()
        {
            return $@"BizType:{BizType}{Environment.NewLine}Description:{Description}{Environment.NewLine}{(string.IsNullOrWhiteSpace(ClassName) ?"":$"ClassName:{ClassName}{Environment.NewLine}")}{(string.IsNullOrWhiteSpace(EventNo) ? "" : $"EventNo:{EventNo}{Environment.NewLine}")}{(TopContext==null ? "" : $"TopContext:{TopContext.SerializeToJSON()}{Environment.NewLine}")}";
        }
    }



}
