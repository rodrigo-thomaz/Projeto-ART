namespace ART.Domotica.Repository.Entities
{
    using System;

    public class HardwaresInProject
    {
        #region Properties

        public Project Project
        {
            get; set;
        }

        public Guid ProjectId
        {
            get; set;
        }

        public HardwaresInApplication HardwaresInApplication
        {
            get; set;
        }

        public Guid HardwaresInApplicationId
        {
            get; set;
        }

        #endregion Properties
    }
}