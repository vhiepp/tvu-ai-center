const Image = ({ data }) => {
  return (
    <div className="postbox__details-img-box d-flex justify-content-center">
      <div
        className="text-center"
        style={{
          maxWidth: data.stretched ? "100%" : "600px",
        }}
      >
        <img
          className="mb-10"
          src={data.file.url}
          alt="theme-pure"
          style={{
            width: "100%",
            borderRadius: "10px",
            border: data.withBorder ? "2px solid #ccc" : "none",
            ...(data.withBackground && {
              backgroundColor: "#f0f0f0",
              padding: "15px",
              borderRadius: "10px",
            }),
          }}
        />
        <h5
          className="postbox__details-img-caption"
          style={{ lineHeight: "13px" }}
          dangerouslySetInnerHTML={{ __html: data.caption }}
        ></h5>
      </div>
    </div>
  );
};

export default Image;
