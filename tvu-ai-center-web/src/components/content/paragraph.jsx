import { useEffect } from "react";

const Paragraph = ({ data }) => {
  const { text } = data;

  useEffect(() => {
    // Chọn tất cả <a> nằm trong <p class="paragraph">
    const links = document.querySelectorAll("p.paragraph a");

    links.forEach((link) => {
      // Tuỳ chọn: chỉ thêm nếu là link ngoài
      if (link.hostname !== window.location.hostname) {
        link.setAttribute("target", "_blank");
        link.setAttribute("rel", "noopener noreferrer");
      }
    });
  }, []);

  return (
    <div className="postbox__details-title-box">
      <p
        className="paragraph"
        style={{ textAlign: "justify" }}
        dangerouslySetInnerHTML={{ __html: text }}
      ></p>
    </div>
  );
};
export default Paragraph;
