using Nt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.BLL
{
    public class PermissionRecordProvider
    {
        public static string[] AllPermissionCategory
        {
            get
            {
                return new string[] { };
            }
        }

        /// <summary>
        /// all permissions records
        /// </summary>
        public static Permission[] AllPermissionRecords
        {
            get
            {
                return new Permission[] { };
            }
        }

        /// <summary>
        /// to define  the permission that all common administrator owns
        /// </summary>
        public static Permission[] AdminDefaultPermissionRecords
        {
            get
            {
                return new Permission[] { };
            }
        }
    }
}
