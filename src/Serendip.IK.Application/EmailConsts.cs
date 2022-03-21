﻿using Serendip.IK.Emails.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK
{
    public static class EmailConsts
    {
        public static string IKPromotionRequestEmail(IKPromotionEmailDto promotionEmailDto)
        {
            return @"<!DOCTYPE html PUBLIC '-'//W3C//DTD XHTML 1.0 Transitional //EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html
  xmlns='http://www.w3.org/1999/xhtml'
  xmlns:v='urn:schemas-microsoft-com:vml'
  xmlns:o='urn:schemas-microsoft-com:office:office'
>
  <head>
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
    <meta name='x-apple-disable-message-reformatting' />
    <meta http-equiv='X-UA-Compatible' content='IE=edge' />
    <title></title>

    <style type='text/css'>
      table,
      td {
        color: #000000;
      }
      a {
        color: #0000ee;
        text-decoration: underline;
      }
      @media (max-width: 480px) {
        #u_content_image_2 .v-src-width {
          width: auto !important;
        }
        #u_content_image_2 .v-src-max-width {
          max-width: 90% !important;
        }
        #u_content_text_8 .v-container-padding-padding {
          padding: 10px 20px 40px !important;
        }
        #u_content_button_2 .v-size-width {
          width: 96% !important;
        }
      }
      @media only screen and (min-width: 620px) {
        .u-row {
          width: 600px !important;
        }
        .u-row .u-col {
          vertical-align: top;
        }

        .u-row .u-col-100 {
          width: 600px !important;
        }
      }

      @media (max-width: 620px) {
        .u-row-container {
          max-width: 100% !important;
          padding-left: 0px !important;
          padding-right: 0px !important;
        }
        .u-row .u-col {
          min-width: 320px !important;
          max-width: 100% !important;
          display: block !important;
        }
        .u-row {
          width: calc(100% - 40px) !important;
        }
        .u-col {
          width: 100% !important;
        }
        .u-col > div {
          margin: 0 auto;
        }
      }
      body {
        margin: 0;
        padding: 0;
      }

      table,
      tr,
      td {
        vertical-align: top;
        border-collapse: collapse;
      }

      p {
        margin: 0;
      }

      .ie-container table,
      .mso-container table {
        table-layout: fixed;
      }

      * {
        line-height: inherit;
      }

      a[x-apple-data-detectors='true'] {
        color: inherit !important;
        text-decoration: none !important;
      }
    </style>
    <link
      href='https://fonts.googleapis.com/css?family=Raleway:400,700&display=swap'
      rel='stylesheet'
      type='text/css'
    />
  </head>

  <body
    class='clean-body u_body'
    style='
      margin: 0;
      padding: 0;
      -webkit-text-size-adjust: 100%;
      background-color: #ffffff;
      color: #000000;
    '
  >
    <table
      style='
        border-collapse: collapse;
        table-layout: fixed;
        border-spacing: 0;
        mso-table-lspace: 0pt;
        mso-table-rspace: 0pt;
        vertical-align: top;
        min-width: 320px;
        margin: 0 auto;
        background-color: #ffffff;
        width: 100%;
      '
      cellpadding='0'
      cellspacing='0'
    >
      <tbody>
        <tr style='vertical-align: top'>
          <td
            style='
              word-break: break-word;
              border-collapse: collapse !important;
              vertical-align: top;
            '
          >
            <div
              class='u-row-container'
              style='padding: 15px; background-color: transparent'
            >
              <div
                class='u-row'
                style='
                  margin: 0 auto;
                  min-width: 320px;
                  max-width: 600px;
                  overflow-wrap: break-word;
                  word-wrap: break-word;
                  word-break: break-word;
                  background-color: transparent;
                '
              >
                <div
                  style='
                    border-collapse: collapse;
                    display: table;
                    width: 100%;
                    background-color: transparent;
                  '
                >
                  <div
                    class='u-col u-col-100'
                    style='
                      max-width: 320px;
                      min-width: 600px;
                      display: table-cell;
                      vertical-align: top;
                    '
                  >
                    <div
                      style='
                        width: 100% !important;
                        border-radius: 0px;
                        -webkit-border-radius: 0px;
                        -moz-border-radius: 0px;
                      '
                    >
                       <div style='
                          padding: 0px;
                          border-top: 2px solid #ccc;
                          border-left: 2px solid #ccc;
                          border-right: 2px solid #ccc;
                          border-bottom: 2px solid #ccc;
                          border-radius: 0px;
                          -webkit-border-radius: 0px;
                          -moz-border-radius: 0px;
                        '
                      >
                        <table
                          id='u_content_image_2'
                          style='font-family:Raleway, sans-serif'
                          role='presentation'
                          cellpadding='0'
                          cellspacing='0'
                          width='100%'
                          border='0'
                        >
                          <tbody>
                            <tr>
                              <td
                                class='v-container-padding-padding'
                                style='
                                  overflow-wrap: break-word;
                                  word-break: break-word;
                                  padding: 50px 10px 30px;
                                  font-family:Raleway, sans-serif;'
                                align='left'
                              >
                                <table
                                  width='100%'
                                  cellpadding='0'
                                  cellspacing='0'
                                  border='0'
                                >
                                  <tr>
                                    <td
                                      style='
                                        padding-right: 0px;
                                        padding-left: 0px;
                                      '
                                      align='center'
                                    >
                                      <img
                                        align='center'
                                        border='0'
                                        src='https://suratkargo.com.tr/assets/images/mail-images/image-4.png'
                                        alt='Hero Image'
                                        title='Hero Image'
                                        style='
                                          outline: none;
                                          text-decoration: none;
                                          -ms-interpolation-mode: bicubic;
                                          clear: both;
                                          display: inline-block !important;
                                          border: none;
                                          height: auto;
                                          float: none;
                                          width: 70%;
                                          max-width: 420px;
                                        '
                                        width='420'
                                        class='v-src-width v-src-max-width'
                                      />
                                    </td>
                                  </tr>
                                </table>
                              </td>
                            </tr>
                          </tbody>
                        </table>

                        <table
                          style='font-family: Raleway, sans-serif'
                          role='presentation'
                          cellpadding='0'
                          cellspacing='0'
                          width='100%'
                          border='0'
                        >
                          <tbody>
                            <tr>
                              <td
                                class='v-container-padding-padding'
                                style='
                                  overflow-wrap: break-word;
                                  word-break: break-word;
                                  padding: 10px;
                                  font-family: Raleway, sans-serif'
                                align='left'
                              >
                                <h1
                                  style='
                                    margin: 0px;
                                    color: #1a2e35;
                                    line-height: 150%;
                                    text-align: center;
                                    word-wrap: break-word;
                                    font-weight: normal;
                                    font-family: tahoma, arial, helvetica,
                                      sans-serif;
                                    font-size: 35px;
                                  '
                                >
                                  S&uuml;rat Kargo İnsan Kaynakları
                                </h1>
                              </td>
                            </tr>
                          </tbody>
                        </table>

                        <table
                          id='u_content_text_8'
                          style='font-family: Raleway, sans-serif'
                          role='presentation'
                          cellpadding='0'
                          cellspacing='0'
                          width='100%'
                          border='0'
                        >
                          <tbody>
                            <tr>
                              <td
                                class='v-container-padding-padding'
                                style='
                                  overflow-wrap: break-word;
                                  word-break: break-word;
                                  padding: 10px 50px 40px;
                                  font-family: Raleway, sans-serif;
                                '
                                align='left'
                              >
                                <div
                                  style='
                                    color: #2a3808;
                                    line-height: 180%;
                                    text-align: center;
                                    word-wrap: break-word;
                                  '
                                >
                                  <p
                                    style='
                                      font-size: 14px;
                                      line-height: 180%;
                                      text-align: left;
                                    '
                                  >
                                    <strong>Sn. Y&ouml;netici,</strong>
                                  </p>
                                  <p
                                    style='
                                      font-size: 14px;
                                      line-height: 180%;
                                      text-align: left;
                                    '
                                  >
                                    Aşağıda bilgileri bulunan personel
                                    i&ccedil;in terfi s&uuml;reci
                                    başlatılmıştır. Link &uuml;zerinden sisteme
                                    giriş yaparak değerlendirmede
                                    bulunabilirsiniz.
                                  </p>
                                  <p
                                    style='
                                      font-size: 14px;
                                      line-height: 180%;
                                      text-align: left;
                                    '
                                  >
                                    &nbsp;
                                  </p>
                                  <p
                                    style='
                                      font-size: 14px;
                                      line-height: 180%;
                                      text-align: left;
                                    '
                                  >
                                    <strong>SicilNo :</strong> " + promotionEmailDto.RegistrationNumber + @"
                                  </p>
                                  <p
                                    style='
                                      font-size: 14px;
                                      line-height: 180%;
                                      text-align: left;
                                    '
                                  >
                                    <strong>Ad Soyad : </strong> " + promotionEmailDto.FirstAndLastName + @"
                                  </p>
                                  <p
                                    style='
                                      font-size: 14px;
                                      line-height: 180%;
                                      text-align: left;
                                    '
                                  >
                                    <strong>G&ouml;revi : </strong>" + promotionEmailDto.Title + @"
                                  </p>
                                  <p
                                    style='
                                      font-size: 14px;
                                      line-height: 180%;
                                      text-align: left;
                                    '
                                  >
                                    <strong
                                      >Terfi Talep Edilen G&ouml;rev : </strong
                                    >" + promotionEmailDto.PositionRequestedForPromotion + @"
                                  </p>
                                  <p
                                    style='
                                      font-size: 14px;
                                      line-height: 180%;
                                      text-align: left;
                                    '
                                  >
                                    &nbsp;
                                  </p>
                                  <p
                                    style='
                                      font-size: 14px;
                                      line-height: 180%;
                                      text-align: left;
                                    '
                                  >
                                    Saygılarımızla
                                  </p>
                                  <p
                                    style='
                                      font-size: 14px;
                                      line-height: 180%;
                                      text-align: left;
                                    '
                                  >
                                    S&uuml;rat Kargo
                                  </p>
                                </div>
                              </td>
                            </tr>
                          </tbody>
                        </table>

                        <table
                          id='u_content_button_2'
                          style='font-family: Raleway, sans-serif'
                          role='presentation'
                          cellpadding='0'
                          cellspacing='0'
                          width='100%'
                          border='0'
                        >
                          <tbody>
                            <tr>
                              <td
                                class='v-container-padding-padding'
                                style='
                                  overflow-wrap: break-word;
                                  word-break: break-word;
                                  padding: 20px 10px 70px;
                                  font-family: Raleway, sans-serif;
                                '
                                align='left'
                              >
                                <div align='center'>
                                  <a
                                    href='https://unlayer.com'
                                    target='_blank'
                                    class='v-size-width'
                                    style='
                                      box-sizing: border-box;
                                      display: inline-block;
                                      font-family: Raleway, sans-serif;
                                      text-decoration: none;
                                      -webkit-text-size-adjust: none;
                                      text-align: center;
                                      color: #000000;
                                      background-color: #aaff00;
                                      border-radius: 4px;
                                      -webkit-border-radius: 4px;
                                      -moz-border-radius: 4px;
                                      width: 60%;
                                      max-width: 100%;
                                      overflow-wrap: break-word;
                                      word-break: break-word;
                                      word-wrap: break-word;
                                      mso-border-alt: none;
                                    '
                                  >
                                    <span
                                      style='
                                        display: block;
                                        padding: 22px 30px 20px;
                                        line-height: 120%;
                                      '
                                      ><span
                                        style='
                                          font-size: 16px;
                                          line-height: 19.2px;
                                          font-family: tahoma, arial, helvetica,
                                            sans-serif;
                                        '
                                        ><strong
                                          ><span
                                            style='
                                              line-height: 19.2px;
                                              font-size: 16px;
                                            '
                                            ><span
                                              style='
                                                line-height: 19.2px;
                                                font-size: 16px;
                                              '
                                              >DEĞERLENDİR</span
                                            ></span
                                          ></strong
                                        ></span
                                      ></span
                                    >
                                  </a>
                                </div>
                              </td>
                            </tr>
                          </tbody>
                        </table>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <div
              class='u-row-container'
              style='padding: 0px; background-color: #f3efef'
            >
              <div
                class='u-row'
                style='
                  margin: 0 auto;
                  min-width: 320px;
                  max-width: 600px;
                  overflow-wrap: break-word;
                  word-wrap: break-word;
                  word-break: break-word;
                  background-color: transparent;
                '
              >
                <div
                  style='
                    border-collapse: collapse;
                    display: table;
                    width: 100%;
                    background-color: transparent;
                  '
                >
                  <div
                    class='u-col u-col-100'
                    style='
                      max-width: 320px;
                      min-width: 600px;
                      display: table-cell;
                      vertical-align: top;
                    '
                  >
                    <div
                      style='
                        width: 100% !important;
                        border-radius: 0px;
                        -webkit-border-radius: 0px;
                        -moz-border-radius: 0px;
                      '
                    >
                      <div
                        style='
                          padding: 0px;
                          border-top: 0px solid transparent;
                          border-left: 0px solid transparent;
                          border-right: 0px solid transparent;
                          border-bottom: 0px solid transparent;
                          border-radius: 0px;
                          -webkit-border-radius: 0px;
                          -moz-border-radius: 0px;
                        '
                      >
                        <table
                          style='font-family: Raleway, sans-serif'
                          role='presentation'
                          cellpadding='0'
                          cellspacing='0'
                          width='100%'
                          border='0'
                        >
                          <tbody>
                            <tr>
                              <td
                                class='v-container-padding-padding'
                                style='
                                  overflow-wrap: break-word;
                                  word-break: break-word;
                                  padding: 60px 10px 20px;
                                  font-family: Raleway, sans-serif;
                                '
                                align='left'
                              >
                                <div align='center'>
                                  <div style='display: table; max-width: 187px'>
                                    <table
                                      align='left'
                                      border='0'
                                      cellspacing='0'
                                      cellpadding='0'
                                      width='32'
                                      height='32'
                                      style='
                                        border-collapse: collapse;
                                        table-layout: fixed;
                                        border-spacing: 0;
                                        mso-table-lspace: 0pt;
                                        mso-table-rspace: 0pt;
                                        vertical-align: top;
                                        margin-right: 15px;
                                      '
                                    >
                                      <tbody>
                                        <tr style='vertical-align: top'>
                                          <td
                                            align='left'
                                            valign='middle'
                                            style='
                                              word-break: break-word;
                                              border-collapse: collapse !important;
                                              vertical-align: top;
                                            '
                                          >
                                            <a
                                              href='https://facebook.com/suratkargo'
                                              title='Facebook'
                                              target='_blank'
                                            >
                                              <img
                                                src='https://suratkargo.com.tr/assets/images/mail-images/image-1.png'
                                                alt='Facebook'
                                                title='Facebook'
                                                width='32'
                                                style='
                                                  outline: none;
                                                  text-decoration: none;
                                                  -ms-interpolation-mode: bicubic;
                                                  clear: both;
                                                  display: block !important;
                                                  border: none;
                                                  height: auto;
                                                  float: none;
                                                  max-width: 32px !important;
                                                '
                                              />
                                            </a>
                                          </td>
                                        </tr>
                                      </tbody>
                                    </table>
                                    <table
                                      align='left'
                                      border='0'
                                      cellspacing='0'
                                      cellpadding='0'
                                      width='32'
                                      height='32'
                                      style='
                                        border-collapse: collapse;
                                        table-layout: fixed;
                                        border-spacing: 0;
                                        mso-table-lspace: 0pt;
                                        mso-table-rspace: 0pt;
                                        vertical-align: top;
                                        margin-right: 15px;
                                      '
                                    >
                                      <tbody>
                                        <tr style='vertical-align: top'>
                                          <td
                                            align='left'
                                            valign='middle'
                                            style='
                                              word-break: break-word;
                                              border-collapse: collapse !important;
                                              vertical-align: top;
                                            '
                                          >
                                            <a
                                              href='https://twitter.com/suratkargotr'
                                              title='Twitter'
                                              target='_blank'
                                            >
                                              <img
                                                src='https://suratkargo.com.tr/assets/images/mail-images/image-3.png'
                                                alt='Twitter'
                                                title='Twitter'
                                                width='32'
                                                style='
                                                  outline: none;
                                                  text-decoration: none;
                                                  -ms-interpolation-mode: bicubic;
                                                  clear: both;
                                                  display: block !important;
                                                  border: none;
                                                  height: auto;
                                                  float: none;
                                                  max-width: 32px !important;
                                                '
                                              />
                                            </a>
                                          </td>
                                        </tr>
                                      </tbody>
                                    </table>
                                    <table
                                      align='left'
                                      border='0'
                                      cellspacing='0'
                                      cellpadding='0'
                                      width='32'
                                      height='32'
                                      style='
                                        border-collapse: collapse;
                                        table-layout: fixed;
                                        border-spacing: 0;
                                        mso-table-lspace: 0pt;
                                        mso-table-rspace: 0pt;
                                        vertical-align: top;
                                        margin-right: 15px;
                                      '
                                    >
                                      <tbody>
                                        <tr style='vertical-align: top'>
                                          <td
                                            align='left'
                                            valign='middle'
                                            style='
                                              word-break: break-word;
                                              border-collapse: collapse !important;
                                              vertical-align: top;
                                            '
                                          >
                                            <a
                                              href='https://linkedin.com/company/suratkargotr/mycompany/'
                                              title='LinkedIn'
                                              target='_blank'
                                            >
                                              <img
                                                src='https://suratkargo.com.tr/assets/images/mail-images/image-5.png'
                                                alt='LinkedIn'
                                                title='LinkedIn'
                                                width='32'
                                                style='
                                                  outline: none;
                                                  text-decoration: none;
                                                  -ms-interpolation-mode: bicubic;
                                                  clear: both;
                                                  display: block !important;
                                                  border: none;
                                                  height: auto;
                                                  float: none;
                                                  max-width: 32px !important;
                                                '
                                              />
                                            </a>
                                          </td>
                                        </tr>
                                      </tbody>
                                    </table>
                                    <table
                                      align='left'
                                      border='0'
                                      cellspacing='0'
                                      cellpadding='0'
                                      width='32'
                                      height='32'
                                      style='
                                        border-collapse: collapse;
                                        table-layout: fixed;
                                        border-spacing: 0;
                                        mso-table-lspace: 0pt;
                                        mso-table-rspace: 0pt;
                                        vertical-align: top;
                                        margin-right: 0px;
                                      '
                                    >
                                      <tbody>
                                        <tr style='vertical-align: top'>
                                          <td
                                            align='left'
                                            valign='middle'
                                            style='
                                              word-break: break-word;
                                              border-collapse: collapse !important;
                                              vertical-align: top;
                                            '
                                          >
                                            <a
                                              href='https://instagram.com/suratkargotr'
                                              title='Instagram'
                                              target='_blank'
                                            >
                                              <img
                                                src='https://suratkargo.com.tr/assets/images/mail-images/image-2.png'
                                                alt='Instagram'
                                                title='Instagram'
                                                width='32'
                                                style='
                                                  outline: none;
                                                  text-decoration: none;
                                                  -ms-interpolation-mode: bicubic;
                                                  clear: both;
                                                  display: block !important;
                                                  border: none;
                                                  height: auto;
                                                  float: none;
                                                  max-width: 32px !important;
                                                '
                                              />
                                            </a>
                                          </td>
                                        </tr>
                                      </tbody>
                                    </table>
                                  </div>
                                </div>
                              </td>
                            </tr>
                          </tbody>
                        </table>

                        <table
                          style='font-family: Raleway, sans-serif'
                          role='presentation'
                          cellpadding='0'
                          cellspacing='0'
                          width='100%'
                          border='0'
                        >
                          <tbody>
                            <tr>
                              <td
                                class='v-container-padding-padding'
                                style='
                                  overflow-wrap: break-word;
                                  word-break: break-word;
                                  padding: 10px;
                                  font-family: Raleway, sans-serif;
                                '
                                align='left'
                              >
                                <div
                                  style='
                                    line-height: 200%;
                                    text-align: center;
                                    word-wrap: break-word;
                                  '
                                >
                                  <p style='font-size: 14px; line-height: 200%'>
                                    <span
                                      style='font-size: 16px; line-height: 32px'
                                      >0850 202 02 02</span
                                    ><br /><span
                                      style='font-size: 16px; line-height: 32px'
                                      >Bahçelievler, Bosna Bulvarı No: 146,
                                      34688 Üsküdar/İstanbul</span
                                    >
                                  </p>
                                </div>
                              </td>
                            </tr>
                          </tbody>
                        </table>

                        <table
                          style='font-family: Raleway, sans-serif'
                          role='presentation'
                          cellpadding='0'
                          cellspacing='0'
                          width='100%'
                          border='0'
                        >
                          <tbody>
                            <tr>
                              <td
                                class='v-container-padding-padding'
                                style='
                                  overflow-wrap: break-word;
                                  word-break: break-word;
                                  padding: 15px;
                                  font-family: Raleway, sans-serif;
                                '
                                align='left'
                              >
                                <table
                                  height='0px'
                                  align='center'
                                  border='0'
                                  cellpadding='0'
                                  cellspacing='0'
                                  width='78%'
                                  style='
                                    border-collapse: collapse;
                                    table-layout: fixed;
                                    border-spacing: 0;
                                    mso-table-lspace: 0pt;
                                    mso-table-rspace: 0pt;
                                    vertical-align: top;
                                    border-top: 3px solid #ffffff;
                                    -ms-text-size-adjust: 100%;
                                    -webkit-text-size-adjust: 100%;
                                  '
                                >
                                  <tbody>
                                    <tr style='vertical-align: top'>
                                      <td
                                        style='
                                          word-break: break-word;
                                          border-collapse: collapse !important;
                                          vertical-align: top;
                                          font-size: 0px;
                                          line-height: 0px;
                                          mso-line-height-rule: exactly;
                                          -ms-text-size-adjust: 100%;
                                          -webkit-text-size-adjust: 100%;
                                        '
                                      >
                                        <span>&#160;</span>
                                      </td>
                                    </tr>
                                  </tbody>
                                </table>
                              </td>
                            </tr>
                          </tbody>
                        </table>

                        <table
                          style='font-family: Raleway, sans-serif'
                          role='presentation'
                          cellpadding='0'
                          cellspacing='0'
                          width='100%'
                          border='0'
                        >
                          <tbody>
                            <tr>
                              <td
                                class='v-container-padding-padding'
                                style='
                                  overflow-wrap: break-word;
                                  word-break: break-word;
                                  padding: 10px 10px 25px;
                                  font-family: Raleway, sans-serif;
                                '
                                align='left'
                              >
                                <div
                                  style='
                                    line-height: 140%;
                                    text-align: center;
                                    word-wrap: break-word;
                                  '
                                >
                                  <p style='font-size: 14px; line-height: 140%'>
                                    &copy; 2022 Port Kargo A.Ş. T&uuml;m hakları
                                    saklıdır.
                                  </p>
                                </div>
                              </td>
                            </tr>
                          </tbody>
                        </table>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
  </body>
</html>
";
        }
    }
}