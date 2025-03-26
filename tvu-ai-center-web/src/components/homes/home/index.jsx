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
import BlogGrid from "@/components/blog/blog-grid";
import Portfolio2 from "@/components/blog-details/portfolio-2";
import Brand from "@/components/about/brand";

const HomeOne = () => {
  return (
    <>
      <div id="smooth-wrapper" style={{ minHeight: "100vh" }}>
        <div id="smooth-content">
          <main className="fix">
            <HeroArea />

            {/* <FeatureArea /> */}
            <AboutArea />
            <Brand />
            {/* <ServicesArea /> */}
            <Portfolio2 />
            {/* <BlogGrid /> */}
            <ProjectArea />
            {/* <TestimonialArea /> */}
            {/* <RankArea /> */}
            {/* <PriceArea /> */}
          </main>
          {/* <Footer /> */}
          {/* <ScrollToTop /> */}
        </div>
      </div>
    </>
  );
};

export default HomeOne;
