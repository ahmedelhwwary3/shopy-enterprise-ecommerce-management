namespace Enterprise_E_Commerce_Management_System.Models.Variants.Enums
{
    public enum enDisplayType
    {
        Text = 1,        // عرض نص عادي (Weight, Material, Brand)
        Dropdown = 2,    // اختيار واحد (Size, Storage, Capacity)
        Color = 3,       // ألوان (دوائر / مربعات)
        Radio = 4,       // اختيارات قليلة وواضحة
        Checkbox = 5     // Multiple values (Features, Extras)
    }
}
