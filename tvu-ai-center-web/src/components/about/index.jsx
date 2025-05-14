import AboutArea from "@/common/about-area";
import FooterFive from "@/layout/footers/footer-5";
import HeaderSix from "@/layout/headers/header-6";
import React, { useEffect } from "react";
import Breadcrumb from "../../common/breadcrumbs/breadcrumb";
import HeroBanner from "../../common/hero-banner";
import CtaArea from "../contact/cta-area";
import TeamArea from "../homes/home-4/team-area";
import Brand from "./brand";
import CompanyArea from "./company-area";
import JobArea from "./job-area";
import JourneyArea from "./journey-area";
import { useTranslation } from "@/utils/i18n";
import ProjectArea from "../homes/home/project-area";
import ContactFormArea from "../contact/contact-form-area";
import { domain } from "@/apis/apiClient";
import ViewContentEditorJS from "../content/view-content-editorjs";

const About = () => {
  const { t, locale } = useTranslation();
  const [about, setAbout] = React.useState([]);

  const getAboutData = async () => {
    const res = await fetch(`${domain}/${locale}/page-contents/about`);
    const json = await res.json();
    const data = json.data.content.blocks;
    console.log({ data });
    setAbout(data);
  };

  useEffect(() => {
    getAboutData();
  }, []);

  return (
    <>
      <Breadcrumb title_top={t("header.about")} title_bottom="AI Center" />
      <HeroBanner
        title={t("header.about")}
        subtitle="AI Center"
        bg_img="/assets/images/tvu/tvu_anime23.png"
      />
      <div className="container">
        <ViewContentEditorJS data={about} />
      </div>
      <AboutArea />
      <TeamArea bg_style={true} />
      {/* <CompanyArea /> */}
      {/* <ProjectArea /> */}
      <JourneyArea />
      <div style={{ marginBottom: "4rem" }}></div>
      <Brand />
      <ContactFormArea />
      {/* <JobArea /> */}
    </>
  );
};

export default About;
