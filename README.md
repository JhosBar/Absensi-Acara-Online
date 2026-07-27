# 📋 Absensi Acara Online

Aplikasi web berbasis **ASP.NET Core MVC** untuk mengelola absensi peserta acara secara online. Sistem ini memungkinkan admin untuk membuat dan mengelola acara, serta merekap data kehadiran peserta secara real-time.

---

## 🧩 Fitur Utama

- 🔐 Autentikasi admin berbasis **Cookie Authentication**
- 📅 Manajemen **acara/event** (buat, lihat, edit, hapus)
- ✅ Pencatatan **absensi peserta** secara online
- 📊 **Rekap kehadiran** per acara
- 👤 Manajemen **admin** dan **role/permission**
- 📋 Tabel data interaktif menggunakan **DataTables**

---

## 🏗️ Arsitektur Proyek

Solusi ini terdiri dari beberapa proyek yang saling terhubung:

```
Absensi Acara Online (Solution)
├── Absensi Acara Online/   ← Proyek utama (ASP.NET Core MVC)
│   ├── Controllers/        ← Logic permintaan HTTP
│   ├── Models/             ← View Models
│   ├── Views/              ← Tampilan UI (Razor Pages)
│   └── wwwroot/            ← Aset statis (CSS, JS, dll)
│
├── Absensi.EF/             ← Entity Framework (Database Context & Entities)
│   └── Data/
│       └── AbsensiContext.cs
│
├── Absensi.Services/       ← Business Logic Layer
│   ├── EventService.cs
│   ├── AdminService.cs
│   ├── AccountService.cs
│   ├── AttendanceService.cs
│   └── RecapService.cs
│
└── Absensi.LIB/            ← Library Utilitas & Keamanan
    ├── Security.cs
    └── Tool.cs
```

---

## ⚙️ Prasyarat (Prerequisites)

Pastikan semua perangkat lunak berikut telah terinstal sebelum menjalankan aplikasi:

| Perangkat Lunak | Versi Minimum | Link Unduhan |
|---|---|---|
| [.NET SDK](https://dotnet.microsoft.com/download/dotnet/8.0) | **8.0** | https://dotnet.microsoft.com/download/dotnet/8.0 |
| [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) | 2019 / 2022 | https://www.microsoft.com/sql-server |
| [Visual Studio](https://visualstudio.microsoft.com/) | 2022 | https://visualstudio.microsoft.com/ |
| [Git](https://git-scm.com/downloads) | Terbaru | https://git-scm.com/downloads |

> **Catatan:** Saat instalasi Visual Studio, pastikan workload **"ASP.NET and web development"** dicentang.

---

## 🚀 Panduan Instalasi & Menjalankan Aplikasi

### Langkah 1 — Clone Repository

Buka terminal (Command Prompt / PowerShell / Git Bash), lalu jalankan:

```bash
git clone https://github.com/JhosBar/Absensi-Acara-Online.git
cd <nama-repo>
```

---

### Langkah 2 — Persiapkan Database SQL Server

#### 2a. Buat Database

Buka **SQL Server Management Studio (SSMS)** atau gunakan terminal, lalu buat database baru:

```sql
CREATE DATABASE absensidb;
```

#### 2b. Konfigurasi Connection String

Buka file `Absensi.EF/Data/AbsensiContext.cs`, lalu sesuaikan baris connection string berikut dengan konfigurasi SQL Server kamu:

```csharp
// Sebelum (default):
optionsBuilder.UseSqlServer("Server=DSG01;User ID=sa;Password=sasa;Database=absensidb;Trusted_Connection=True;TrustServerCertificate=True;");

// Setelah (sesuaikan):
optionsBuilder.UseSqlServer("Server=NAMA_SERVER_KAMU;User ID=USERNAME;Password=PASSWORD;Database=absensidb;Trusted_Connection=True;TrustServerCertificate=True;");
```

> **Tip:** Ganti `NAMA_SERVER_KAMU` dengan nama instance SQL Server kamu (contoh: `localhost`, `.\SQLEXPRESS`).

---

### Langkah 3 — Jalankan Migrasi Database

Buka terminal di folder root solusi, lalu jalankan perintah berikut untuk membuat tabel-tabel database:

```bash
# Pindah ke direktori proyek EF
cd Absensi.EF

# Jalankan migrasi (jika sudah ada migrasi)
dotnet ef database update

# Atau jika belum ada migrasi, buat migrasi baru terlebih dahulu:
dotnet ef migrations add InitialCreate
dotnet ef database update
```

> **Catatan:** Pastikan kamu sudah menginstal EF Tools. Jika belum, jalankan:
> ```bash
> dotnet tool install --global dotnet-ef
> ```

---

### Langkah 4 — Restore Dependensi NuGet

Kembali ke folder root solusi dan jalankan:

```bash
cd ..
dotnet restore
```

Atau melalui **Visual Studio**: klik kanan pada Solution → **Restore NuGet Packages**.

---

### Langkah 5 — Build Proyek

```bash
dotnet build
```

Pastikan tidak ada error pada output. Jika berhasil, kamu akan melihat pesan:
```
Build succeeded.
```

---

### Langkah 6 — Jalankan Aplikasi

#### Melalui Terminal

```bash
cd "Absensi Acara Online"
dotnet run
```

#### Melalui Visual Studio

1. Buka file `Invitation.sln` di Visual Studio 2022
2. Set **"Absensi Acara Online"** sebagai **Startup Project** (klik kanan → *Set as Startup Project*)
3. Tekan tombol **▶ Run** atau tekan `F5`

---

### Langkah 7 — Buka Aplikasi di Browser

Setelah aplikasi berjalan, buka browser dan akses:

```
https://localhost:7XXX
```

> Port yang digunakan akan terlihat di output terminal setelah `dotnet run`, misalnya:
> ```
> Now listening on: https://localhost:7128
> ```

---

## 🔑 Akun Default

Setelah database berhasil dibuat, daftarkan akun admin pertama langsung melalui database atau sesuai ketentuan yang ada di seed data.

| Field | Nilai |
|---|---|
| URL Login | `/account/login` |

---

## 🛠️ Tech Stack

| Teknologi | Keterangan |
|---|---|
| **ASP.NET Core MVC 8.0** | Framework web utama |
| **Entity Framework Core 8.0** | ORM untuk akses database |
| **SQL Server** | Database relasional |
| **Newtonsoft.Json** | Serialisasi JSON |
| **Cookie Authentication** | Sistem autentikasi |
| **DataTables** | Tabel interaktif di UI |

---

## 📁 Struktur Tabel Database

| Tabel | Keterangan |
|---|---|
| `mtAdmin` | Data admin/pengguna sistem |
| `mtRole` | Role/jabatan admin |
| `mtEvent` | Data acara/event |
| `mtPermission` | Daftar permission menu |
| `pRolePermission` | Relasi role dan permission |
| `pRecap` | Rekap absensi peserta |
| `pTransaction` | Data transaksi/log |

---

## 🤝 Kontribusi

Pull request sangat disambut! Untuk perubahan besar, harap buka *issue* terlebih dahulu untuk mendiskusikan apa yang ingin diubah.

---

## 📄 Lisensi

Proyek ini dibuat untuk keperluan **Tugas Kuliah Semester 2**.
