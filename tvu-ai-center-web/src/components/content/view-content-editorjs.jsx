import HeaderContent from "./header";
import Qoute from "./quote";
import Paragraph from "./paragraph";
import Delimiter from "./delimiter";
import List from "./list";
import Image from "./image";
import Table from "./table";
import Code from "./code";

const ViewContentEditorJS = ({ data }) => {
  return (
    <div className="postbox__details-wrapper pr-20">
      <article>
        {data.map((item, i) => {
          if (item.type === "header") {
            return (
              <HeaderContent key={`${i}-${Math.random()}`} data={item.data} />
            );
          } else if (item.type === "paragraph") {
            return <Paragraph key={`${i}-${Math.random()}`} data={item.data} />;
          } else if (item.type === "quote") {
            return <Qoute key={`${i}-${Math.random()}`} data={item.data} />;
          } else if (item.type === "delimiter") {
            return <Delimiter key={`${i}-${Math.random()}`} data={item.data} />;
          } else if (item.type === "list") {
            return <List key={`${i}-${Math.random()}`} data={item.data} />;
          } else if (item.type === "image") {
            return <Image key={`${i}-${Math.random()}`} data={item.data} />;
          } else if (item.type === "table") {
            return <Table key={`${i}-${Math.random()}`} data={item.data} />;
          } else if (item.type === "code") {
            return <Code key={`${i}-${Math.random()}`} data={item.data} />;
          }
        })}
        {/* <div className="postbox__details-title-box pb-30">
          <h4 className="postbox__details-title">{title_1}</h4>
          <p>{des_1}</p>
          <p>{des_2}</p>
        </div>
        <div className="postbox__details-checkmark">
          <ul>
            {checkmark_list.map((item, i) => (
              <li key={i}>
                <i className="fal fa-check"></i>
                {item}
              </li>
            ))}
          </ul>
        </div>
        <div className="postbox__details-title-box pb-30">
          <h4 className="postbox__details-title">{title_2}</h4>
          <p>{des_3}</p>
        </div>
        <div className="postbox__details-img-box d-flex">
          <div className="mr-20 text-center">
            <Image
              className="mb-20"
              src={blog_details_img_2}
              alt="theme-pure"
            />
            <h4 className="postbox__details-img-caption">
              <span>Images by</span>@sample
            </h4>
          </div>
          <div className="text-center">
            <Image
              className="mb-20"
              src={blog_details_img_3}
              alt="theme-pure"
            />
            <h5 className="postbox__details-img-caption">
              <span>Images by</span>@sample
            </h5>
          </div>
        </div>
        <div className="postbox__details-title-box pb-15">
          <p>{des_4}</p>
        </div>
        <div className="postbox__details-qoute mb-30">
          <blockquote className="d-flex align-items-start">
            <div className="postbox__details-qoute-icon">
              <DoubleSemicolon />
            </div>
            <div className="postbox__details-qoute-text">
              <p>
                “The team at @softecagency is incredibly dedicated,
                knowledgeable, and helpful.
              </p>
              <span>Socrates</span>
            </div>
          </blockquote>
        </div>
        <div className="postbox__details-title-box pb-15">
          <p>{des_5}</p>
        </div> */}
      </article>
    </div>
  );
};

export default ViewContentEditorJS;
