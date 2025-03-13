import useSticky from "@/hooks/use-sticky";
import Offcanvus from "@/common/offcanvus";
import Image from "next/image";
import Link from "next/link";
import React, { useState } from "react";
import NavMenu from "./nav-menu";
import { useTranslation } from "@/utils/i18n";

import white_logo from "../../../public/assets/images/logo/tvu.png";
import black_logo from "../../../public/assets/images/logo/tvu.png";

const hero_content = {
  login_btn: "Login",
  sign_up_btn: "Sign Up",
};
const { login_btn, sign_up_btn } = hero_content;

const HeaderTwo = () => {
  const { sticky } = useSticky();
  const [sidebarOpen, setSidebarOpen] = useState(false);

  const { t } = useTranslation();

  return (
    <>
      <header className="tp-header-height">
        <div
          id="header-sticky"
          className={`"tp-header-2__area header-sticky-bg-2 tp-header-2__transparent tp-header-2__plr z-index-3 ${
            sticky && "header-sticky"
          }`}
        >
          <div className="container-fluid g-0">
            <div className="row g-0 align-items-center">
              <div className="col-xl-2 col-lg-2 col-md-6 col-6">
                <div className="tp-header-2__logo">
                  <Link className="white-logo" href="/">
                    <div className="logo">
                      <Image
                        src={white_logo}
                        alt="theme-pure"
                        style={{ height: "48px", width: "48px" }}
                      />
                      <span>AI Center</span>
                    </div>
                  </Link>

                  <Link className="black-logo logo" href="/">
                    <div className="logo">
                      <Image
                        src={black_logo}
                        alt="theme-pure"
                        style={{ height: "48px", width: "48px" }}
                      />
                      <span>AI Center</span>
                    </div>
                  </Link>
                </div>
              </div>
              <div className="col-xl-8 col-lg-8 d-none d-lg-block">
                <div className="tp-header-2__main-menu text-center">
                  <nav id="mobile-menu">
                    <NavMenu />
                  </nav>
                </div>
              </div>
              <div className="col-xl-2 col-lg-2 col-md-6 col-6">
                <div className="tp-header-2__right d-flex align-items-center justify-content-end">
                  <Link
                    className="tp-btn-green-sm d-none d-md-block"
                    href="/sign-in"
                  >
                    {t("login")}
                  </Link>
                  <a
                    className="header-bottom__bar tp-menu-bar d-lg-none"
                    onClick={() => setSidebarOpen(true)}
                  >
                    <i className="fal fa-bars"></i>
                  </a>
                </div>
              </div>
            </div>
          </div>
        </div>
      </header>
      <Offcanvus sidebarOpen={sidebarOpen} setSidebarOpen={setSidebarOpen} />
    </>
  );
};

export default HeaderTwo;
