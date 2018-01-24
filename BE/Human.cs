﻿namespace dotNet5778_Project_5337_7178
{
    namespace BE
    {
        using System;

        /// <summary>
        /// Defines the <see cref="Human" />
        /// </summary>
        public class Human : Id
        {
            #region Fields

            private string address;
            private DateTime dateOfBirth;
            private string firstName;
            private bool isFemale;
            private string lastName;
            private string phoneNumber;

            #endregion

            #region Properties

            public string Address
            {
                get => this.address; set => this.address = value ?? throw new ArgumentNullException(nameof(this.Address));
            }
            public DateTime DateOfBirth
            {
                get => this.dateOfBirth; set => this.dateOfBirth = value <= DateTime.Now && DateTime.Now < value.AddDays(120) ? value : throw new ArgumentOutOfRangeException(nameof(this.DateOfBirth));
            }
            public string FirstName
            {
                get => this.firstName; set => this.firstName = value ?? throw new ArgumentNullException(nameof(this.FirstName));
            }
            public bool IsFemale
            {
                get => this.isFemale; set => this.isFemale = value;
            }
            public string LastName
            {
                get => this.lastName; set => this.lastName = value ?? throw new ArgumentNullException(nameof(this.LastName));
            }
            public string PhoneNumber
            {
                get => phoneNumber; set => phoneNumber = value;
            }

            #endregion

            #region Methods

            /// <summary>
            /// The ToString
            /// </summary>
            /// <returns>The <see cref="string"/></returns>
            public override string ToString() => $"Type:{this.GetType().Name} ID:{this.ID}, first name:{this.FirstName}, last name:{this.LastName}, address:{this.Address}.";

            #endregion

            /// <summary>
            /// Initializes a new instance of the <see cref="Human"/> class.
            /// </summary>
            /// <param name="iD">The <see cref="int"/></param>
            /// <param name="dateOfBirth">The <see cref="DateTime"/></param>
            /// <param name="firstName">The <see cref="string"/></param>
            /// <param name="lastName">The <see cref="string"/></param>
            /// <param name="isFemale">The <see cref="bool"/></param>
            /// <param name="address">The <see cref="string"/></param>
            /// <param name="phoneNumber">The <see cref="string"/></param>
            public Human(int iD, DateTime dateOfBirth, string firstName, string lastName, bool isFemale, string address, string phoneNumber) : base(iD)
            {
                this.Address = address;
                this.DateOfBirth = dateOfBirth;
                this.FirstName = firstName;
                this.LastName = lastName;
                this.PhoneNumber = phoneNumber;
                this.IsFemale = isFemale;
                if (!(iD >= 0 && iD < 1000000000))
                    throw new ArgumentOutOfRangeException(nameof(this.ID));
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Human"/> class.
            /// </summary>
            /// <param name="iD">The <see cref="long"/></param>
            public Human(long iD = 0) : base(iD)
            {
                if (!(iD >= 0 && iD < 1000000000))
                    throw new ArgumentOutOfRangeException(nameof(this.ID));
            }
        }
    }
}