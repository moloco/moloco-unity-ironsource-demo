//  https://stackoverflow.com/questions/55492214/the-annotation-for-nullable-reference-types-should-only-be-used-in-code-within-a
#nullable enable

using System;
using UnityEngine;

namespace MolocoSdk 
{
    public class AndroidDelegate : PlatformDelegate
    {
        internal const string PACKAGE_PREFIX = "com.moloco.sdk.publisher";

        public void SetPrivacy(PrivacySettings privacySettings){
            var molocoPrivacyJavaClasspath = "com.moloco.sdk.adapter.MolocoPrivacy";
            
            using (var MolocoPrivacyJavaClass = new AndroidJavaClass(molocoPrivacyJavaClasspath))
            using (var privacySettingsJavaObject = new AndroidJavaObject($"{molocoPrivacyJavaClasspath}$PrivacySettings"))
            {
                privacySettingsJavaObject.Set("isUserConsent", privacySettings.IsUserConsent.ToBooleanJavaObject());
                privacySettingsJavaObject.Set("isAgeRestrictedUser", privacySettings.IsAgeRestrictedUser.ToBooleanJavaObject());
                privacySettingsJavaObject.Set("isDoNotSell", privacySettings.IsDoNotSell.ToBooleanJavaObject());

                MolocoPrivacyJavaClass.CallStatic("setPrivacy", privacySettingsJavaObject);
            }
        } 
    }
    internal static class BooleanExtensions 
    {
        internal static AndroidJavaObject? ToBooleanJavaObject(this Boolean? me) 
        {
            using (var BooleanJavaClass = new AndroidJavaClass("java.lang.Boolean")){
                var trueJavaObject = BooleanJavaClass.GetStatic<AndroidJavaObject>("TRUE");
                var falseJavaObject = BooleanJavaClass.GetStatic<AndroidJavaObject>("FALSE");
                
                if (me == true) return trueJavaObject;
                if (me == false) return falseJavaObject;
            }

            return  null;
        }
    }    
}
