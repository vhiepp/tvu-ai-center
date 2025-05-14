const List = ({ data }) => {
  const { items, style } = data;

  //   const listStyle = {
  //     listStyleType: style === "ordered" ? "decimal" : "disc",
  //     paddingLeft: "20px",
  //   };

  return (
    <>
      {(style === "unordered" || style === "ordered") && (
        <div
          className={`pd-details-overview`}
          style={{ paddingBottom: "20px" }}
        >
          <ul style={{ paddingBottom: 0 }}>
            {items?.map((list, i) => {
              if (style === "unordered") {
                return (
                  <li
                    key={`list-${i}-${Math.random()}`}
                    style={{ listStyleType: "disc" }}
                    dangerouslySetInnerHTML={{
                      __html: list.content,
                    }}
                  ></li>
                );
              } else if (style === "ordered") {
                return (
                  <li
                    key={`list-${i}-${Math.random()}`}
                    style={{ listStyleType: "decimal" }}
                    dangerouslySetInnerHTML={{
                      __html: list.content,
                    }}
                  ></li>
                );
              }
            })}
          </ul>
        </div>
      )}
      {style === "checklist" && (
        <div className="postbox__details-checkmark">
          <ul>
            {items?.map((list, i) => (
              <li
                key={`list-${i}-${Math.random()}`}
                style={{ fontWeight: 400 }}
                dangerouslySetInnerHTML={{
                  __html: `<i
                  class="fal fa-check"
                  style='
                    top: "2px";
                    background-color: ${
                      list.meta.checked ? "#5138EE" : "#c1c1c1"
                    }'
                ></i> ${list.content}`,
                }}
              ></li>
            ))}
          </ul>
        </div>
      )}
    </>
  );
};

export default List;
