import BreadcrumbTwo from "@/common/breadcrumbs/breadcrumb-2";
import React from "react";
import BlogGrid from "./blog-grid";
import Portfolio from "./portfolio";
import { useTranslation } from "@/utils/i18n";

const Blog = () => {
  const { t } = useTranslation();
  return (
    <>
      <main>
        <BreadcrumbTwo title={t("latest_news")} innertitle={t("header.news")} />
        {/* <BlogGrid /> */}
        <Portfolio />
      </main>
    </>
  );
};

export default Blog;
