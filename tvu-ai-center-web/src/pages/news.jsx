import React from "react";
import SEO from "../common/seo";
import Blog from "../components/blog";
import Wrapper from "../layout/wrapper";
import { useTranslation } from "@/utils/i18n";

const index = () => {
  const { t } = useTranslation();
  return (
    <Wrapper>
      <SEO pageTitle={t("header.news")} />
      <Blog />
    </Wrapper>
  );
};

export default index;
