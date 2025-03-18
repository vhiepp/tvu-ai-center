import React from "react";
import SEO from "../common/seo";
import Wrapper from "../layout/wrapper";
import Research from "@/components/research";
import { useTranslation } from "@/utils/i18n";

const indx = () => {
  const { t } = useTranslation();

  return (
    <Wrapper>
      <SEO pageTitle={t("header.research")} />
      <Research />
    </Wrapper>
  );
};

export default indx;
