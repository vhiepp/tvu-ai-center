import React from "react";
import Image from "next/image";
import useBreadcrumbTitleAnime from "@/hooks/useBreadcrumbTitleAnime";

// spahe import here
import shape_1 from "../../../public/assets/img/breadcrumb/breadcrumb-shape-2-2.png";
import shape_2 from "../../../public/assets/img/breadcrumb/breadcrumb-sub-2.png";
import shape_3 from "../../../public/assets/img/breadcrumb/breadcrumb-sub-3.png";
import { useTranslation } from "@/utils/i18n";

const BreadcrumbSeven = () => {
  const { animeRef } = useBreadcrumbTitleAnime();
  const { t } = useTranslation();
  return (
    <>
      <div className="breadcrumb__area breadcrumb-height-2 breadcrumb-overlay p-relative fix">
        <div className="breadcrumb__shape-2 z-index-4">
          <Image src={shape_1} alt="theme-pure" />
        </div>
        <div className="breadcrumb__shape-3 z-index-4">
          <Image src={shape_2} alt="theme-pure" />
        </div>
        <div className="breadcrumb__shape-4 z-index-4">
          <Image src={shape_3} alt="theme-pure" />
        </div>
        <div className="container">
          <div className="row justify-content-center">
            <div className="col-xl-10">
              <div className="breadcrumb__content z-index-3 text-center">
                <h3
                  ref={animeRef}
                  className="breadcrumb__title tp-char-animation text-white anime_text"
                >
                  {t("header.research")}
                </h3>
                <div
                  className="breadcrumb__text text-white wow tpfadeUp"
                  data-wow-duration=".9s"
                  data-wow-delay=".6s"
                >
                  <p style={{ color: "#d7d7d7" }}>
                    {t("ai_research_applications")}
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default BreadcrumbSeven;
