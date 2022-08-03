using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia.Class.DictionariesHanding
{
   
        public class ClassPermission
        {
           
            private int permissionId;
            private string permission;

           
            public int PermissionId { get => permissionId; set => permissionId = value; }
            public string Permission { get => permission; set => permission = value; }

        public ClassPermission()
        {

        }
        public ClassPermission(string permissionName)
        {
            this.permission= permissionName;
        }


        public override string ToString()
        {
            return Permission;
        }

    }
}
