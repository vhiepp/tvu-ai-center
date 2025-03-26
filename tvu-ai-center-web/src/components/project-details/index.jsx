import BreadcrumbEight from "@/common/breadcrumbs/breadcrumb-8";
import FooterFive from "@/layout/footers/footer-5";
import HeaderSix from "@/layout/headers/header-6";
import React from "react";
import ProjectArea from "../homes/home/project-area";
import ProjectDetailsArea from "./project-details-area";
import ThumbArea from "./thumb-area";

const ProjectDetails = () => {
  return (
    <>
      {/* <div id="smooth-wrapper">
        <div id="smooth-content"> */}
      <main>
        <BreadcrumbEight />
        <ThumbArea />
        <ProjectDetailsArea />
        {/* <ProjectArea /> */}
      </main>
      {/* </div>
      </div> */}
    </>
  );
};

export default ProjectDetails;
