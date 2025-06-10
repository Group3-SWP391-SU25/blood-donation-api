using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Utilities
{
    public static class BloodCheckUtils
    {
        public static List<string> ValidateBloodIndexes(
        double wbc, double rbc, double hgb, double hct,
        double mcv, double mch, double mchc, double plt, double mpv)
        {
            var errors = new List<string>();

            if (wbc < 4.0 || wbc > 10.0) errors.Add($"WBC ({wbc}) ngoài khoảng 4.0 - 10.0");
            if (rbc < 4.2 || rbc > 6.1) errors.Add($"RBC ({rbc}) ngoài khoảng 4.2 - 6.1");
            if (hgb < 12.5 || hgb > 17.5) errors.Add($"HGB ({hgb}) ngoài khoảng 12.5 - 17.5");
            if (hct < 36 || hct > 52) errors.Add($"HCT ({hct}) ngoài khoảng 36 - 52");
            if (mcv < 80 || mcv > 100) errors.Add($"MCV ({mcv}) ngoài khoảng 80 - 100");
            if (mch < 27 || mch > 33) errors.Add($"MCH ({mch}) ngoài khoảng 27 - 33");
            if (mchc < 32 || mchc > 36) errors.Add($"MCHC ({mchc}) ngoài khoảng 32 - 36");
            if (plt < 150 || plt > 450) errors.Add($"PLT ({plt}) ngoài khoảng 150 - 450");
            if (mpv < 7.5 || mpv > 11.5) errors.Add($"MPV ({mpv}) ngoài khoảng 7.5 - 11.5");

            return errors;
        }

        public static List<string> ValidateAll(
            double wbc, double rbc, double hgb, double hct,
            double mcv, double mch, double mchc, double plt, double mpv)
        {
            var allErrors = new List<string>();
            allErrors.AddRange(ValidateBloodIndexes(wbc, rbc, hgb, hct, mcv, mch, mchc, plt, mpv));
            return allErrors;
        }
    }
}
