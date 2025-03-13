import { CopyRight, SocialLinksTwo } from "@/common/social-links";
import EmailIcon from "@/svg/email";
import EmailThree from "@/svg/email-3";
import LocationTwo from "@/svg/location-2";
import PhoneThree from "@/svg/phone-3";
import RightArrow from "@/svg/right-arrow";
import Link from "next/link";
import Image from "next/image";
import React, { useEffect, useState } from "react";
import { useTranslation, languages, getCurrentLocaleName } from "@/utils/i18n";
import { useRouter } from "next/router";

import footer_shape from "../../../public/assets/img/footer/footer-bottom-shape-4-1.png";
import footer_logo from "../../../public/assets/img/logo/logo-white.png";

const footer_content = {
  sub_title: "SUBSCRIBE TO OUR NEWSLETTER",
  policy_text: (
    <>
      By subscribing, you accepted the our <a href="#">Policy</a>
    </>
  ),
  phone: "0123456789",
  email: "ai@tvu.edu.vn",
  location: "https://maps.app.goo.gl/8zQFiz3UzJT6sKcF9",

  footer_lisks: [
    { name: "Test page", link: "#" },
    { name: "Test page", link: "#" },
    { name: "Test page", link: "#" },
    { name: "Test page", link: "#" },
    { name: "Test page", link: "#" },
    //  { name: "FAQ", link: "/faq" },
    //  { name: "About", link: "/about" },
    //  { name: "Pricing Plan", link: "/price" },
    //  { name: "Integrations", link: "/integrations" },
  ],
};
const { sub_title, policy_text, phone, email, location, footer_lisks } =
  footer_content;

const FooterFour = () => {
  const [isOppen, setIsOppen] = useState(false);
  const [langNow, setLangNow] = useState("");

  const { t } = useTranslation();
  const router = useRouter();

  const oppenLan = () => {
    setIsOppen(!isOppen);
  };

  const changeLanguage = (lang) => {
    if (lang === "vi") {
      window.location.href = `/${router.asPath === "/" ? "" : router.asPath}`;
    } else {
      window.location.href = `/${lang}${
        router.asPath === "/" ? "" : router.asPath
      }`;
    }
    //  oppenLan();
  };

  useEffect(() => {
    setLangNow(getCurrentLocaleName());
  }, []);

  return (
    <>
      <footer>
        <div className="tp-footer__pl-pr blue-bg z-index fix">
          <div className="tp-footer__area tp-footer__border-bottom-4 fix">
            <div className="tp-footer-bottom-shape">
              <Image src={footer_shape} alt="theme-pure" />
            </div>
            <div className="container">
              <div className="row">
                <div
                  className="col-xl-4 col-lg-4 col-md-6 pb-45 wow tpfadeUp"
                  data-wow-duration=".2s"
                  data-wow-delay=".1s"
                >
                  <div className="tp-footer__widget footer-widget-4 footer-col-4-1">
                    <div className="tp-footer__logo mb-25">
                      <Link href="/">
                        <Image src={footer_logo} alt="theme-pure" />
                      </Link>
                    </div>
                    <h3 className="footer-subtitle-3">
                      {t("subscribe_newsletter")}
                    </h3>
                    <div className="tp-footer__input mb-15 p-relative">
                      <form onSubmit={(e) => e.preventDefault()}>
                        <input
                          type="text"
                          placeholder={t("your_email")}
                          required
                        />
                        <span>
                          <EmailIcon />
                        </span>
                        <button>
                          <RightArrow />
                        </button>
                      </form>
                    </div>
                    {/* <p>{policy_text}</p> */}
                    <div className="tp-team-social">
                      <SocialLinksTwo />
                    </div>
                  </div>
                </div>
                <div
                  className="col-xl-5 col-lg-5 col-md-6 pb-45 wow tpfadeUp"
                  data-wow-duration=".2s"
                  data-wow-delay=".2s"
                >
                  <div className="tp-footer__widget footer-widget-4 footer-col-4-2">
                    <h4 className="tp-footer__widget-title">
                      {t("header.products")}
                    </h4>
                    <div className="tp-footer__content">
                      <ul>
                        {footer_lisks.map((item, i) => (
                          <li key={i}>
                            <Link href={item.link}>{item.name}</Link>
                          </li>
                        ))}
                      </ul>
                    </div>
                  </div>
                </div>
                <div
                  className="col-xl-3 col-lg-3 col-md-6 pb-45 wow tpfadeUp"
                  data-wow-duration=".2s"
                  data-wow-delay=".4s"
                >
                  <div className="tp-footer__widget footer-widget-4 footer-col-4-3">
                    <h4 className="tp-footer__widget-title">
                      {t("contact_info")}
                    </h4>
                    <div className="tp-contact-info-box">
                      <ul>
                        <li>
                          <PhoneThree />
                          <Link href={`tel:${phone}`}>{phone}</Link>
                        </li>
                        <li>
                          <EmailThree />
                          <Link href={`mailto:${email}`}>{email}</Link>
                        </li>
                        <li>
                          <LocationTwo />{" "}
                          <Link href={location} target="_blank">
                            {t("address")}
                          </Link>
                        </li>
                      </ul>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div
            className="tp-copyright__area pt-20 pb-20 wow tpfadeUp"
            data-wow-duration=".2s"
            data-wow-delay=".3s"
          >
            <div className="container">
              <div className="row justify-content-center">
                <div className="col-xl-4 col-lg-3 col-md-3"></div>
                <div className="col-xl-4 col-lg-6 col-md-6">
                  <div className="tp-copyright__text tp-copyright__text-4 text-center">
                    <span>
                      <CopyRight />
                    </span>
                  </div>
                </div>
                <div className="col-xl-4 col-lg-3 col-md-3">
                  <div className="tp-copyright__lang-box  d-flex align-items-center justify-content-md-end justify-content-sm-end justify-content-start">
                    <div className="tp-copyright__lang tp-copyright__lang-2">
                      <ul>
                        <li>
                          <button
                            id="tp-copyright__lang-toggle"
                            onClick={() => oppenLan()}
                          >
                            <span>
                              {langNow}
                              <i className="fal fa-angle-down"></i>
                            </span>
                          </button>
                          {isOppen && (
                            <ul
                              className={`tp-copyright__lang-submenu ${
                                isOppen && "open"
                              }`}
                            >
                              {languages.map((lang, i) => (
                                <li key={`lang-${i}`}>
                                  <span
                                    style={{ cursor: "pointer" }}
                                    onClick={() => changeLanguage(lang.code)}
                                  >
                                    {lang.name}
                                  </span>
                                </li>
                              ))}
                            </ul>
                          )}
                        </li>
                      </ul>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </footer>
    </>
  );
};

export default FooterFour;
