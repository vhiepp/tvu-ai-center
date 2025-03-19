import Link from "next/link";
import Image from "next/image";
import React from "react";

// images import
import about_img_1 from "../../public/assets/img/about/about-bg-shape.png";
import about_img_2 from "../../public/assets/images/ai3.jpg";
import about_img_3 from "../../public/assets/images/ai4.png";
import about_img_4 from "../../public/assets/images/ai2.jpg";
import about_img_5 from "../../public/assets/img/about/about-5.png";
import { useTranslation } from "@/utils/i18n";

// about data
const about_data = [
  {
    id: 1,
    cls: "bg-shape",
    img: about_img_1,
  },
  {
    id: 2,
    cls: "main-img z-index",
    img: about_img_2,
  },
  {
    id: 3,
    cls: "sub-img-1 d-none d-sm-block z-index-3",
    img: about_img_3,
  },
  {
    id: 4,
    cls: "sub-img-2 d-none d-sm-block",
    img: about_img_4,
  },
  {
    id: 5,
    cls: "sub-img-3 d-none d-sm-block z-index-3",
    img: about_img_5,
  },
];

const AboutArea = () => {
  const { t } = useTranslation();

  const about_list = [
    t("research_development"),
    t("ai_model_training"),
    t("apply_ai_to_the_system"),
  ];

  return (
    <>
      <div className="tp-about__area tp-about__pt-pb pt-100 pb-160">
        <div className="container">
          <div className="row align-items-center">
            <div
              className="col-xl-6 col-lg-6 wow tpfadeLeft"
              data-wow-duration=".9s"
              data-wow-delay=".2s"
            >
              <div className="tp-about__img-wrapper text-center text-lg-end p-relative">
                {about_data.map((item, i) => (
                  <div key={i} className={`tp-about__${item.cls}`}>
                    <Image src={item.img} alt="theme-pure" />
                  </div>
                ))}
              </div>
            </div>
            <div
              className="col-xl-6 col-lg-6 wow tpfadeRight"
              data-wow-duration=".9s"
              data-wow-delay=".6s"
            >
              <div className="tp-about__right">
                <div className="tp-about__section-box">
                  <h4 className="tp-section-subtitle">{t("function")}</h4>
                  <h3 className="tp-section-title mb-15">
                    {t("research_and_development_ai")}
                  </h3>
                  {/* <p>{des}</p> */}
                </div>
                <div className="tp-about__list">
                  <ul>
                    {about_list.map((item, i) => (
                      <li key={i}>
                        <i className="fal fa-check"></i>
                        {item}
                      </li>
                    ))}
                  </ul>
                </div>
                <div className="tp-about__btn">
                  <Link
                    className="tp-btn tp-btn-hover alt-color-black"
                    href="/about"
                  >
                    <span>{t("header.contact")}</span>
                    <b></b>
                  </Link>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default AboutArea;
