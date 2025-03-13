"use client";

import React from "react";
import SEO from "../common/seo";
import HomeOne from "../components/homes/home";
import Wrapper from "../layout/wrapper";
import { useTranslation } from "@/utils/i18n";

const Home = () => {
  const { t } = useTranslation();

  return (
    <Wrapper>
      <SEO pageTitle={t("header.home")} />
      <HomeOne />
    </Wrapper>
  );
};

export default Home;
