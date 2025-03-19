import AboutArea from "@/common/about-area";
import FooterFive from "@/layout/footers/footer-5";
import HeaderSix from "@/layout/headers/header-6";
import React from "react";
import Breadcrumb from "../../common/breadcrumbs/breadcrumb";
import HeroBanner from "../../common/hero-banner";
import CtaArea from "../contact/cta-area";
import TeamArea from "../homes/home-4/team-area";
import Brand from "./brand";
import CompanyArea from "./company-area";
import JobArea from "./job-area";
import JourneyArea from "./journey-area";
import { useTranslation } from "@/utils/i18n";

const About = () => {
  const { t } = useTranslation();

  return (
    <>
      <Breadcrumb title_top={t("header.about")} title_bottom="AI Center" />
      <HeroBanner
        title={t("header.about")}
        subtitle="AI Center"
        bg_img="/assets/images/tvu/tvu_anime23.png"
      />
      <AboutArea />
      <Brand />
      <TeamArea bg_style={true} />
      {/* <CompanyArea /> */}
      <JourneyArea />
      <JobArea />
    </>
  );
};

export default About;
