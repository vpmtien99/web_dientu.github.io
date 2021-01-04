namespace WebApplication30.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TMDT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChiTietHoaDons",
                c => new
                    {
                        MaCTHD = c.Int(nullable: false, identity: true),
                        DonGia = c.Double(nullable: false),
                        SoLuong = c.Int(nullable: false),
                        ThanhTien = c.Double(nullable: false),
                        Flag = c.Boolean(nullable: false),
                        HDId = c.Int(nullable: false),
                        KinhId = c.Int(nullable: false),
                        HoaDon_MaHD = c.Int(),
                        Kinh_MaKinh = c.Int(),
                    })
                .PrimaryKey(t => t.MaCTHD)
                .ForeignKey("dbo.HoaDons", t => t.HoaDon_MaHD)
                .ForeignKey("dbo.Kinhs", t => t.Kinh_MaKinh)
                .Index(t => t.HoaDon_MaHD)
                .Index(t => t.Kinh_MaKinh);
            
            CreateTable(
                "dbo.HoaDons",
                c => new
                    {
                        MaHD = c.Int(nullable: false, identity: true),
                        NgayTao = c.DateTime(nullable: false),
                        TongTien = c.Double(nullable: false),
                        TenNguoiNhan = c.String(),
                        SDT = c.String(),
                        DiaChi = c.String(),
                        TinhTrangDonHang = c.String(),
                        TinhTrangHoaDon = c.String(),
                        Flag = c.Boolean(nullable: false),
                        KHId = c.Int(nullable: false),
                        KhachHang_MaKH = c.Int(),
                    })
                .PrimaryKey(t => t.MaHD)
                .ForeignKey("dbo.KhachHangs", t => t.KhachHang_MaKH)
                .Index(t => t.KhachHang_MaKH);
            
            CreateTable(
                "dbo.KhachHangs",
                c => new
                    {
                        MaKH = c.Int(nullable: false, identity: true),
                        Ho = c.String(),
                        Ten = c.String(),
                        NgaySinh = c.DateTime(nullable: false),
                        GioiTinh = c.Boolean(nullable: false),
                        Username = c.String(),
                        Password = c.String(),
                        DiemTichLuy = c.Int(nullable: false),
                        LoaiTK = c.String(),
                        Flag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MaKH);
            
            CreateTable(
                "dbo.DanhGias",
                c => new
                    {
                        MaDanhGia = c.Int(nullable: false, identity: true),
                        BinhLuan = c.String(),
                        Ngay = c.DateTime(nullable: false),
                        Flag = c.Boolean(nullable: false),
                        KHId = c.Int(nullable: false),
                        KinhId = c.Int(nullable: false),
                        KhachHang_MaKH = c.Int(),
                        Kinh_MaKinh = c.Int(),
                    })
                .PrimaryKey(t => t.MaDanhGia)
                .ForeignKey("dbo.KhachHangs", t => t.KhachHang_MaKH)
                .ForeignKey("dbo.Kinhs", t => t.Kinh_MaKinh)
                .Index(t => t.KhachHang_MaKH)
                .Index(t => t.Kinh_MaKinh);
            
            CreateTable(
                "dbo.Kinhs",
                c => new
                    {
                        MaKinh = c.Int(nullable: false, identity: true),
                        Gia = c.Double(nullable: false),
                        TenKinh = c.String(),
                        SoLuong = c.Int(nullable: false),
                        AnhBia = c.String(),
                        Anh1 = c.String(),
                        Anh2 = c.String(),
                        LuotXem = c.Long(nullable: false),
                        LuotThich = c.Long(nullable: false),
                        MoTa = c.String(),
                        NgayXB = c.DateTime(nullable: false),
                        Flag = c.Boolean(nullable: false),
                        LoaiId = c.Int(nullable: false),
                        NSXId = c.Int(nullable: false),
                        KhuyenMaiId = c.Int(nullable: false),
                        KhuyenMai_MaKhuyenMai = c.Int(),
                        Loai_MaLoai = c.Int(),
                        NSX_MaNSX = c.Int(),
                    })
                .PrimaryKey(t => t.MaKinh)
                .ForeignKey("dbo.KhuyenMais", t => t.KhuyenMai_MaKhuyenMai)
                .ForeignKey("dbo.Loais", t => t.Loai_MaLoai)
                .ForeignKey("dbo.NSXes", t => t.NSX_MaNSX)
                .Index(t => t.KhuyenMai_MaKhuyenMai)
                .Index(t => t.Loai_MaLoai)
                .Index(t => t.NSX_MaNSX);
            
            CreateTable(
                "dbo.KhuyenMais",
                c => new
                    {
                        MaKhuyenMai = c.Int(nullable: false, identity: true),
                        TenKM = c.String(),
                        PhanTram = c.Double(nullable: false),
                        NgayBD = c.DateTime(nullable: false),
                        NgayKT = c.DateTime(nullable: false),
                        AnhKM = c.String(),
                        Flag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MaKhuyenMai);
            
            CreateTable(
                "dbo.Loais",
                c => new
                    {
                        MaLoai = c.Int(nullable: false, identity: true),
                        TenLoai = c.String(),
                        Flag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MaLoai);
            
            CreateTable(
                "dbo.NSXes",
                c => new
                    {
                        MaNSX = c.Int(nullable: false, identity: true),
                        TenNSX = c.String(),
                        MoTa = c.String(),
                        Anh = c.String(),
                        flag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MaNSX);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        MaLog = c.Int(nullable: false, identity: true),
                        HanhDong = c.String(),
                        ChiTiet = c.String(),
                        Ngay = c.DateTime(nullable: false),
                        Flag = c.Boolean(nullable: false),
                        KHId = c.Int(nullable: false),
                        KhachHang_MaKH = c.Int(),
                    })
                .PrimaryKey(t => t.MaLog)
                .ForeignKey("dbo.KhachHangs", t => t.KhachHang_MaKH)
                .Index(t => t.KhachHang_MaKH);
            
            CreateTable(
                "dbo.TichLuys",
                c => new
                    {
                        MaTichLuy = c.Int(nullable: false, identity: true),
                        Diem = c.Int(nullable: false),
                        PhanTram = c.Double(nullable: false),
                        Flag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MaTichLuy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", "KhachHang_MaKH", "dbo.KhachHangs");
            DropForeignKey("dbo.HoaDons", "KhachHang_MaKH", "dbo.KhachHangs");
            DropForeignKey("dbo.Kinhs", "NSX_MaNSX", "dbo.NSXes");
            DropForeignKey("dbo.Kinhs", "Loai_MaLoai", "dbo.Loais");
            DropForeignKey("dbo.Kinhs", "KhuyenMai_MaKhuyenMai", "dbo.KhuyenMais");
            DropForeignKey("dbo.DanhGias", "Kinh_MaKinh", "dbo.Kinhs");
            DropForeignKey("dbo.ChiTietHoaDons", "Kinh_MaKinh", "dbo.Kinhs");
            DropForeignKey("dbo.DanhGias", "KhachHang_MaKH", "dbo.KhachHangs");
            DropForeignKey("dbo.ChiTietHoaDons", "HoaDon_MaHD", "dbo.HoaDons");
            DropIndex("dbo.Logs", new[] { "KhachHang_MaKH" });
            DropIndex("dbo.Kinhs", new[] { "NSX_MaNSX" });
            DropIndex("dbo.Kinhs", new[] { "Loai_MaLoai" });
            DropIndex("dbo.Kinhs", new[] { "KhuyenMai_MaKhuyenMai" });
            DropIndex("dbo.DanhGias", new[] { "Kinh_MaKinh" });
            DropIndex("dbo.DanhGias", new[] { "KhachHang_MaKH" });
            DropIndex("dbo.HoaDons", new[] { "KhachHang_MaKH" });
            DropIndex("dbo.ChiTietHoaDons", new[] { "Kinh_MaKinh" });
            DropIndex("dbo.ChiTietHoaDons", new[] { "HoaDon_MaHD" });
            DropTable("dbo.TichLuys");
            DropTable("dbo.Logs");
            DropTable("dbo.NSXes");
            DropTable("dbo.Loais");
            DropTable("dbo.KhuyenMais");
            DropTable("dbo.Kinhs");
            DropTable("dbo.DanhGias");
            DropTable("dbo.KhachHangs");
            DropTable("dbo.HoaDons");
            DropTable("dbo.ChiTietHoaDons");
        }
    }
}
