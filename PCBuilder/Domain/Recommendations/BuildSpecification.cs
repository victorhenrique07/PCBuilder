using PCBuilder.Domain.Products.CaseAndCooling;
using PCBuilder.Domain.Products.Cpus;
using PCBuilder.Domain.Products.Graphics;
using PCBuilder.Domain.Products.MotherboardAndMemory;
using PCBuilder.Domain.Products.Peripherals;
using PCBuilder.Domain.Products.Shared;
using System.Collections.Generic;

namespace PCBuilder.Domain.Recommendations
{
    public class BuildSpecification
    {
        public BuildSpecification(IReadOnlyCollection<decimal> budgets, UseProfile useProfile, FpsTarget fpsTarget)
        {
            this.Budgets = budgets;
            this.UseProfile = useProfile;
            this.FpsTarget = fpsTarget;
            this.CpuSockets = new List<CpuSocket>();
            this.NeedsIntegratedGpu = false;
            this.IntegratedGpus = new List<Gpu>();
            this.CpuCoolerCoolingTypes = new List<CoolingType>();
            this.MotherboardFormFactors = new List<MotherboardFormFactor>();
            this.Chipsets = new List<MotherboardChipset>();
            this.MemorySlots = new List<int>();
            this.M2Slots = new List<int>();
            this.WirelessLanStandards = new List<WirelessLanStandard>();
            this.MemoryModuleConfigurations = new List<MemoryModuleConfiguration>();
            this.MemoryTypes = new List<MemoryType>();
            this.VideoCardChipsets = new List<VideoCardChipset>();
            this.GpuManufacturers = new List<Manufacturer>();
            this.StorageDeviceCategories = new List<ProductCategory>();
            this.StorageDeviceInterfaces = new List<PeripheralInterface>();
            this.StorageDeviceCapacities = new List<int>();
            this.PowerSupplyFormFactors = new List<PowerSupplyFormFactor>();
            this.PowerSupplyPowersSupplied = new List<decimal>();
            this.FanDiameters = new List<int>();
            this.DisplaySizes = new List<decimal>();
            this.DisplayResolutions = new List<DisplayResolution>();
            this.DisplayPanelTypes = new List<PanelType>();
            this.DisplayRefreshRates = new List<int>();
            this.KeyboardFormFactors = new List<string>();
            this.KeyboardLayouts = new List<string>();
            this.KeyboardSwitchTypes = new List<string>();
            this.KeyboardSwitchManufacturers = new List<Manufacturer>();
            this.MouseSensors = new List<string>();
            this.Colors = new List<ProductColor>();
            this.LedTypes = new List<LedType?>();
            this.Manufacturers = new List<Manufacturer>();
        }

        public IReadOnlyCollection<decimal> Budgets { get; }

        public UseProfile UseProfile { get; }

        public FpsTarget FpsTarget { get; }

        public IReadOnlyCollection<CpuSocket> CpuSockets { get; set; }

        public bool NeedsIntegratedGpu { get; set; }

        public IReadOnlyCollection<Gpu> IntegratedGpus { get; set; }

        public IReadOnlyCollection<CoolingType> CpuCoolerCoolingTypes { get; set; }

        public IReadOnlyCollection<MotherboardFormFactor> MotherboardFormFactors { get; set; }

        public IReadOnlyCollection<MotherboardChipset> Chipsets { get; set; }

        public IReadOnlyCollection<int> MemorySlots { get; set; }

        public IReadOnlyCollection<int> M2Slots { get; set; }

        public IReadOnlyCollection<WirelessLanStandard> WirelessLanStandards { get; set; }

        public IReadOnlyCollection<MemoryModuleConfiguration> MemoryModuleConfigurations { get; set; }

        public IReadOnlyCollection<MemoryType> MemoryTypes { get; set; }

        public IReadOnlyCollection<VideoCardChipset> VideoCardChipsets { get; set; }

        public IReadOnlyCollection<Manufacturer> GpuManufacturers { get; set; }

        public IReadOnlyCollection<ProductCategory> StorageDeviceCategories { get; set; }

        public IReadOnlyCollection<PeripheralInterface> StorageDeviceInterfaces { get; set; }

        public IReadOnlyCollection<int> StorageDeviceCapacities { get; set; }

        public IReadOnlyCollection<PowerSupplyFormFactor> PowerSupplyFormFactors { get; set; }

        public IReadOnlyCollection<decimal> PowerSupplyPowersSupplied { get; set; }

        public bool? PowerSupplyIsModular { get; set; }

        public IReadOnlyCollection<int> FanDiameters { get; set; }

        public IReadOnlyCollection<decimal> DisplaySizes { get; set; }

        public IReadOnlyCollection<DisplayResolution> DisplayResolutions { get; set; }

        public IReadOnlyCollection<PanelType> DisplayPanelTypes { get; set; }

        public IReadOnlyCollection<int> DisplayRefreshRates { get; set; }

        public IReadOnlyCollection<string> KeyboardFormFactors { get; set; }

        public IReadOnlyCollection<string> KeyboardLayouts { get; set; }

        public IReadOnlyCollection<string> KeyboardSwitchTypes { get; set; }

        public IReadOnlyCollection<Manufacturer> KeyboardSwitchManufacturers { get; set; }

        public IReadOnlyCollection<string> MouseSensors { get; set; }

        public IReadOnlyCollection<ProductColor> Colors { get; set; }

        public IReadOnlyCollection<LedType?> LedTypes { get; set; }

        public IReadOnlyCollection<Manufacturer> Manufacturers { get; set; }
    }
}