const Table = ({ data }) => {
  return (
    <div className="mb-30" style={{ overflowX: "auto" }}>
      <table
        style={{
          width: "100%",
          maxWidth: data.stretched ? "100%" : "700px",
          borderCollapse: "collapse",
          tableLayout: "fixed",
          borderRadius: "10px",
          overflow: "hidden",
          border: "1px solid #ccc",
          display: "block",
          minWidth: "600px",
          margin: "0 auto",
        }}
      >
        {data.withHeadings && (
          <thead>
            <tr>
              {data.content[0].map((cell, i) => {
                return (
                  <th
                    key={`th-${i}-${Math.random()}`}
                    style={{
                      backgroundColor: "#f0f0f0",
                      border: "1px solid #ccc",
                      padding: "10px",
                      textAlign: "left",
                      color: "#5138ee",
                    }}
                    dangerouslySetInnerHTML={{
                      __html: cell,
                    }}
                  ></th>
                );
              })}
            </tr>
          </thead>
        )}
        <tbody>
          {(data.withHeadings ? data.content.slice(1) : data.content).map(
            (row, irow) => {
              return (
                <tr key={`row-${irow}-${Math.random()}`}>
                  {row.map((cell, icell) => {
                    return (
                      <td
                        key={`td-${icell}-${Math.random()}`}
                        style={{
                          border: "1px solid #ccc",
                          padding: "10px",
                          textAlign: "left",
                          verticalAlign: "top",
                        }}
                        dangerouslySetInnerHTML={{
                          __html: cell,
                        }}
                      ></td>
                    );
                  })}
                </tr>
              );
            }
          )}
        </tbody>
      </table>
    </div>
  );
};

export default Table;
