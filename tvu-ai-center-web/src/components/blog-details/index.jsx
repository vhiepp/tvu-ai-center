import BreadcrumbSix from "@/common/breadcrumbs/breadcrumb-6";
import FooterFive from "@/layout/footers/footer-5";
import HeaderSix from "@/layout/headers/header-6";
import React from "react";
import Banner from "./banner";
import Portfolio from "./portfolio";
import PostboxArea from "./postbox-area";

const BlogDetails = () => {
  return (
    <>
      <div id="smooth-wrapper">
        <div id="smooth-content">
          <main>
            <BreadcrumbSix />
            <Banner />
            <PostboxArea />
            <Portfolio />
          </main>
        </div>
      </div>
    </>
  );
};

export default BlogDetails;
