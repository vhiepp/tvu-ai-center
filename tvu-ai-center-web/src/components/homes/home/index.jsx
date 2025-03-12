import ScrollToTop from "@/hooks/scroll-to-top";
// import Footer from "@/layout/footers/footer";
import HeaderTwo from "@/layout/headers/header-2";
import React from "react";
import AboutArea from "../../../common/about-area";
import FeatureArea from "./feature-area";
import HeroSlider from "./hero-slider";
import PriceArea from "./price-area";
import ProjectArea from "./project-area";
import HeroArea from "../home-4/hero-area";
import RankArea from "./rank-area";
import ServicesArea from "./services-area";
import TestimonialArea from "./testimonial-area";
import Footer from "@/layout/footers/footer";

const HomeOne = () => {  
  return (
    <>
      <HeaderTwo />
      <div id="smooth-wrapper">
        <div id="smooth-content">
          <main className="fix">
            <HeroArea />
            
            {/* <FeatureArea /> */}
            {/* <AboutArea /> */}
            {/* <ServicesArea /> */}
            {/* <ProjectArea /> */}
            {/* <TestimonialArea /> */}
            {/* <RankArea /> */}
            {/* <PriceArea /> */}
          </main>
          {/* <Footer /> */}
          <ScrollToTop />
        </div>
      </div>
    </>
  );
};

export default HomeOne;
