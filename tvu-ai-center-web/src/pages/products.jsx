import React from "react";
import SEO from "../common/seo";
import Project from "../components/project";
import Wrapper from "../layout/wrapper";
import { useTranslation } from "@/utils/i18n";

const indx = () => {
  const { t } = useTranslation();

  return (
    <Wrapper>
      <SEO pageTitle={t("header.products")} />
      <Project />
    </Wrapper>
  );
};

export default indx;
