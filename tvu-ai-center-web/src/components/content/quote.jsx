import DoubleSemicolon from "@/svg/double-semicolon";

const Qoute = ({ data }) => {
  const { alignment, text, caption } = data;
  return (
    <div className="postbox__details-qoute mb-15">
      <blockquote className="d-flex align-items-start">
        <div className="postbox__details-qoute-icon">
          <DoubleSemicolon />
        </div>
        <div className="postbox__details-qoute-text">
          <p
            style={{ textAlign: alignment }}
            dangerouslySetInnerHTML={{ __html: text }}
          ></p>
          <span dangerouslySetInnerHTML={{ __html: caption }}></span>
        </div>
      </blockquote>
    </div>
  );
};

export default Qoute;
