using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemporaryGameCompany
{
    [CreateAssetMenu(menuName = "Runtime Sets/Player Container")]
    public class SwordRuntimeContainer : RuntimeSet<SwordControl>
    {
        public SwordControl Get()
        {
            return this.Items[0];
        }
    }
}
