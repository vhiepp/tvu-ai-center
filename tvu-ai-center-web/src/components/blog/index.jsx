import BreadcrumbTwo from "@/common/breadcrumbs/breadcrumb-2";
import FooterFive from "@/layout/footers/footer-5";
import HeaderSix from "@/layout/headers/header-6";
import React from "react";
import CtaArea from "../contact/cta-area";
import BlogGrid from "./blog-grid";
import Portfolio from "./portfolio";
import { useTranslation } from "@/utils/i18n";

const Blog = () => {
  const { t } = useTranslation();
  return (
    <>
      <main>
        <BreadcrumbTwo title={t("latest_news")} innertitle={t("header.news")} />
        <BlogGrid />
        <Portfolio />
      </main>
    </>
  );
};

export default Blog;
