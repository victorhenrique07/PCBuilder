using System;
using System.Collections.Generic;
using System.Linq;

namespace PCBuilder.Domain.Products.Shared
{
    public class ProductCategory : IEquatable<ProductCategory>
    {
        public virtual int Code { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual ProductCategory ParentCategory { get; protected set; }

        public virtual decimal EstimatedShippingCost { get; protected set; }

        public virtual string LocalName { get; protected set; }

        public virtual string PluralLocalName { get; protected set; }

        public virtual bool Available { get; protected set; }

        public virtual int? ProductReferenceId { get; protected set; }

        public virtual short? PopularityRank { get; protected set; }

        public virtual int MaxBuildQuantity { get; protected set; }

        protected ProductCategory() { }

        public ProductCategory(int code, string name, ProductCategory parentCategory, string localName,
            string pluralLocalName)
        {
            this.Code = code;
            this.Name = name;
            this.ParentCategory = parentCategory;
            this.LocalName = localName;
            this.PluralLocalName = pluralLocalName;
        }

        public override string ToString()
        {
            return Name;
        }

        public virtual bool Equals(ProductCategory other)
        {
            if (other == null)
                return false;

            return this.Code.Equals(other.Code);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ProductCategory);
        }

        public override int GetHashCode()
        {
            return this.Code.GetHashCode();
        }

        public static ProductCategory Headphone = new ProductCategory(17, nameof(Headphone), null, string.Empty, string.Empty);

        public static ProductCategory Keyboard = new ProductCategory(20, nameof(Keyboard), null, string.Empty, string.Empty);

        public static ProductCategory Mouse = new ProductCategory(29, nameof(Mouse), null, string.Empty, string.Empty);

        public static ProductCategory Case = new ProductCategory(1, nameof(Case), null, string.Empty, string.Empty);

        public static ProductCategory Chair = new ProductCategory(3, nameof(Chair), null, string.Empty, string.Empty);

        public static ProductCategory Console = new ProductCategory(4, nameof(Console), null, string.Empty, string.Empty);

        public static ProductCategory CpuCooler = new ProductCategory(2, nameof(CpuCooler), null, string.Empty, string.Empty);

        public static ProductCategory Cpu = new ProductCategory(5, nameof(Cpu), null, string.Empty, string.Empty);

        public static ProductCategory Desk = new ProductCategory(6, nameof(Desk), null, string.Empty, string.Empty);

        public static ProductCategory Earbuds = new ProductCategory(7, nameof(Earbuds), Headphone, string.Empty, string.Empty);

        public static ProductCategory Fan = new ProductCategory(8, nameof(Fan), null, string.Empty, string.Empty);

        public static ProductCategory Game = new ProductCategory(14, nameof(Game), null, string.Empty, string.Empty);

        public static ProductCategory GameController = new ProductCategory(10, nameof(GameController), null, string.Empty, string.Empty);

        public static ProductCategory GamingChair = new ProductCategory(12, nameof(GamingChair), Chair, string.Empty, string.Empty);

        public static ProductCategory GamingKeyboard = new ProductCategory(13, nameof(GamingKeyboard), Keyboard, string.Empty, string.Empty);

        public static ProductCategory GamingMouse = new ProductCategory(15, nameof(GamingMouse), Mouse, string.Empty, string.Empty);

        public static ProductCategory HardDrive = new ProductCategory(16, nameof(HardDrive), null, string.Empty, string.Empty);
       
        public static ProductCategory InEarHeadphone = new ProductCategory(19, nameof(InEarHeadphone), Headphone, string.Empty, string.Empty);

        public static ProductCategory KeyboardAndMouseKit = new ProductCategory(21, nameof(KeyboardAndMouseKit), null, string.Empty, string.Empty);

        public static ProductCategory Headset = new ProductCategory(18, nameof(Headset), Headphone, string.Empty, string.Empty);

        public static ProductCategory Laptop = new ProductCategory(18, nameof(Laptop), Laptop, string.Empty, string.Empty);

        public static ProductCategory Memory = new ProductCategory(23, nameof(Memory), null, string.Empty, string.Empty);

        public static ProductCategory Microphone = new ProductCategory(26, nameof(Microphone), null, string.Empty, string.Empty);

        public static ProductCategory Monitor = new ProductCategory(27, nameof(Monitor), null, string.Empty, string.Empty);

        public static ProductCategory MonitorSupport = new ProductCategory(25, nameof(MonitorSupport), null, string.Empty, string.Empty);

        public static ProductCategory Motherboard = new ProductCategory(24, nameof(Motherboard), null, string.Empty, string.Empty);            

        public static ProductCategory Mousepad = new ProductCategory(28, nameof(Mousepad), null, string.Empty, string.Empty);

        public static ProductCategory OpticalDrive = new ProductCategory(33, nameof(OpticalDrive), null, string.Empty, string.Empty);

        public static ProductCategory OtherProduct = new ProductCategory(31, nameof(OtherProduct), null, string.Empty, string.Empty);

        public static ProductCategory OverEarHeadphone = new ProductCategory(34, nameof(OverEarHeadphone), Headphone, string.Empty, string.Empty);

        public static ProductCategory PowerStrip = new ProductCategory(38, nameof(PowerStrip), null, string.Empty, string.Empty);

        public static ProductCategory PowerSupply = new ProductCategory(37, nameof(PowerSupply), null, string.Empty, string.Empty);

        public static ProductCategory Smartphone = new ProductCategory(42, nameof(Smartphone), null, string.Empty, string.Empty);

        public static ProductCategory SoundCard = new ProductCategory(40, nameof(SoundCard), null, string.Empty, string.Empty);

        public static ProductCategory Speaker = new ProductCategory(39, nameof(Peripherals.Speaker), null, string.Empty, string.Empty);

        public static ProductCategory Ssd = new ProductCategory(43, nameof(Ssd), null, string.Empty, string.Empty);

        public static ProductCategory TV = new ProductCategory(45, nameof(TV), null, string.Empty, string.Empty);

        public static ProductCategory ThermalPaste = new ProductCategory(44, nameof(ThermalPaste), null, string.Empty, string.Empty);

        public static ProductCategory VideoCard = new ProductCategory(49, nameof(VideoCard), null, string.Empty, string.Empty);

        public static ProductCategory VideoCardSupport = new ProductCategory(51, nameof(VideoCardSupport), null, string.Empty, string.Empty);

        public static ProductCategory Webcam = new ProductCategory(53, nameof(Webcam), null, string.Empty, string.Empty);

        public static ProductCategory WirelessAdapter = new ProductCategory(52, nameof(WirelessAdapter), null, string.Empty, string.Empty);

        public static ProductCategory ProductKit = new ProductCategory(36, nameof(ProductKit), null, "Kit", string.Empty);

        public static IReadOnlyCollection<ProductCategory> All = new List<ProductCategory>
        {
            Case,
            Console,
            CpuCooler,
            Cpu,
            Earbuds,
            Fan,
            Game,
            GameController,
            HardDrive,
            Headphone,
            InEarHeadphone,
            Keyboard,
            KeyboardAndMouseKit,
            Memory,
            Monitor,
            Motherboard,
            Mouse,
            OpticalDrive,
            OtherProduct,
            OverEarHeadphone,
            PowerSupply,
            Smartphone,
            SoundCard,
            Speaker,
            Ssd,
            ThermalPaste,
            VideoCard,
            VideoCardSupport,
            Webcam,
            WirelessAdapter,
            ProductKit,
        };

        public static IReadOnlyCollection<ProductCategory> Main = new List<ProductCategory>
        {
            CpuCooler,
            Cpu,
            Memory,
            Motherboard,
            VideoCard
        };

        public static IReadOnlyCollection<ProductCategory> Complementary = All.Except(Main).ToList();

        //public static ProductCategory FindById(short id)
        //{
        //    return All.SingleOrDefault(t => t.Code == id);
        //}
    }
}