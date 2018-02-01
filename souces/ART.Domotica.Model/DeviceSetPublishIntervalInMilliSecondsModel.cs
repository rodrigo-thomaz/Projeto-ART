﻿namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceSetPublishIntervalInMilliSecondsModel
    {
        #region Properties

        public DeviceDatasheetEnum DeviceDatasheetId
        {
            get; set;
        }

        public Guid DeviceId
        {
            get; set;
        }

        public int PublishIntervalInMilliSeconds
        {
            get; set;
        }

        #endregion Properties
    }
}