const List = ({ data }) => {
  const { items, style } = data;

  //   const listStyle = {
  //     listStyleType: style === "ordered" ? "decimal" : "disc",
  //     paddingLeft: "20px",
  //   };

  return (
    <div className={`pd-details-overview`} style={{ paddingBottom: "20px" }}>
      <ul style={{ paddingBottom: 0 }}>
        {items?.map((list, i) => (
          <li key={`list-${i}-${Math.random()}`} style={{ listStyleType: "" }}>
            {list.content}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default List;
