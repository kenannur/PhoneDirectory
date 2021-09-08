﻿using System;

namespace ContactApi.Shared.Entities
{
    public class ContactInformation : EntityBase
    {
        public Guid ContactId { get; set; }

        public InformationType Type { get; set; }

        public string Value { get; set; }
    }

    public enum InformationType
    {
        Unknown = 0,
        PhoneNumber = 1,
        EmailAddress = 2,
        Location = 3
    }
}