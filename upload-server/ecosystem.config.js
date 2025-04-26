module.exports = {
  apps: [
    {
      name: "upload-server", // Đặt tên ứng dụng
      script: "npm",
      args: "start", // Chạy lệnh npm start
      cwd: "./", // Đường dẫn thư mục của ứng dụng (mặc định là hiện tại)
    },
  ],
};
