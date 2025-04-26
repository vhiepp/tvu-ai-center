const express = require("express");
const multer = require("multer");
const cors = require("cors");
const fs = require("fs");
const path = require("path");

const app = express();
const PORT = 3020;

// Cho phép CORS
app.use(cors());

// Tạo thư mục uploads nếu chưa có
const UPLOADS_FOLDER = path.join(__dirname, "uploads");
if (!fs.existsSync(UPLOADS_FOLDER)) {
  fs.mkdirSync(UPLOADS_FOLDER);
}

// Cấu hình multer để lưu file
const storage = multer.diskStorage({
  destination: (req, file, cb) => {
    cb(null, UPLOADS_FOLDER);
  },
  filename: (req, file, cb) => {
    const uniqueSuffix = Date.now() + "-" + Math.round(Math.random() * 1e9);
    const ext = path.extname(file.originalname);
    cb(null, file.fieldname + "-" + uniqueSuffix + ext);
  },
});

const upload = multer({ storage });

// Serve static files (xem ảnh trực tiếp)
app.use("/uploads", express.static(UPLOADS_FOLDER));

// API: Upload ảnh
app.post("/upload", upload.single("image"), (req, res) => {
  if (!req.file) {
    return res.status(400).json({ error: "No file uploaded" });
  }

  const fileUrl = `${req.protocol}://${req.get("host")}/uploads/${
    req.file.filename
  }`;
  res.json({
    message: "Upload successful",
    fileUrl,
  });
});

// API: Lấy danh sách ảnh
app.get("/images", (req, res) => {
  fs.readdir(UPLOADS_FOLDER, (err, files) => {
    if (err) {
      return res.status(500).json({ error: "Unable to scan uploads folder" });
    }

    const fileUrls = files.map((file) => ({
      filename: file,
      url: `${req.protocol}://${req.get("host")}/uploads/${file}`,
    }));

    res.json(fileUrls);
  });
});

// API: Xoá ảnh
app.delete("/images/:filename", (req, res) => {
  const filename = req.params.filename;
  const filePath = path.join(UPLOADS_FOLDER, filename);

  fs.unlink(filePath, (err) => {
    if (err) {
      return res.status(404).json({ error: "File not found" });
    }
    res.json({ message: "File deleted successfully" });
  });
});

// Khởi động server
app.listen(PORT, () => {
  console.log(`🚀 Image Server running at http://localhost:${PORT}`);
});
