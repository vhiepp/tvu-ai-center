const font = ["36px", "32px", "28px", "24px", "20px", "16px"];

const HeaderContent = ({ data }) => {
  const { text, level } = data;

  const style = {
    fontSize: font[level - 1],
  };

  return (
    <div className="postbox__details-title-box pb-15">
      {level === 1 && (
        <h1
          className="postbox__details-title"
          style={style}
          dangerouslySetInnerHTML={{ __html: text }}
        ></h1>
      )}
      {level === 2 && (
        <h2
          className="postbox__details-title"
          style={style}
          dangerouslySetInnerHTML={{ __html: text }}
        ></h2>
      )}
      {level === 3 && (
        <h3
          className="postbox__details-title"
          style={style}
          dangerouslySetInnerHTML={{ __html: text }}
        ></h3>
      )}
      {level === 4 && (
        <h4
          className="postbox__details-title"
          style={style}
          dangerouslySetInnerHTML={{ __html: text }}
        ></h4>
      )}
      {level === 5 && (
        <h5
          className="postbox__details-title"
          style={style}
          dangerouslySetInnerHTML={{ __html: text }}
        ></h5>
      )}
      {level === 6 && (
        <h6
          className="postbox__details-title"
          style={style}
          dangerouslySetInnerHTML={{ __html: text }}
        ></h6>
      )}
    </div>
  );
};

export default HeaderContent;
