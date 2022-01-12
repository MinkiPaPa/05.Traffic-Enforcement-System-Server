# Traffic/Speed Enforcement System Server
Author, Code by MinkiPaPa.

# 1. Project Objective
   - 프로젝트 수행 기간 : 2019년 5월 1일 ~ 2019년 10월 30일
   - 프로젝트 참여 인력 : PMO 1 , Dev 2 ( Client, Server 개발 )
   - 프로젝트 수행 목표
      - 수동 단속 프로세스의 자동화
      - 자동차 번호판 인식 기능의 적용 ( OpenALPR 적용 )
      - 남아공 eNaTis ( National Traffic Information System )과 데이터 연동
      - 시스템이 적용되는 각 도시별 결제 이력 연동을 위한 ERP 연동
   - 기타 공통 정보는 Client Repository Readme 참조 [LINK](https://github.com/MinkiPaPa/04.Traffic-Enforcement-System-Client#trafficspeed-enforcement-system-client)
   
# 2. Solution Process Diagram
![image](https://user-images.githubusercontent.com/97417837/149051915-aaedd548-14dd-4596-90d7-bb174409529c.png)

# 3. DataBase Structure 
   |DB table name|Content|Status|
   |---|---|---|
   |CONTRAVENTIONS|메인 단속정보 테이블||
   |HISTORY_OPERATION|진행단계 History  테이블||
   |HISTORY_NOTICENUM|고지서번호 History 테이블||
   |HISTORY_CHANGE|요금 및 이의제기, 법정등 상태변경 History테이블||
   |HISTORY_REPRESENTATION|이의 제기 관련 History 테이블||
   |HISTORY_PAY|범칙금 관련 Histrory 테이블||
   |NUMBER_RECEIPT|요금접수번호 생성관련 테이블||
   |NUMBER_NATIS|Natis 파일 시퀀스번호 생성관련 테이블||
   |NUMBER_NOTICE|Notice 번호 생성 관련 테이블||
   |NUMBER_EASYPAY|EasyPay 번호 생성 관련 테이블||
   |USERS|시, 경찰 접속 사용자 테이블||

# 4. Module Architect
   |Module|Content|Type|Derendence|Status|
   |---|---|---|---|---|
   |TRA.iTOPS.Launcher.exe|프로그램의 시작점, 전역 에러체크, UI설정 및 로그인 화면 실행|exe|TRA.iTOPS.Contracts,  TRA.iTOPS.Windows.Set, TRA.iTOPS.Diagnostics.dll|완료|
   |TRA.iTOPS.Contracts.dll|공통관련 라이브러리(공통함수, 예외처리 등)|Dll|N/A|완료|
   |TRA.iTOPS.Diagnostics.dll|애플리케이션 오류 로그 관련 라이브러리|Dll|N/A|완료|
   |TRA.iTOPS.Service.Core.dll|DB(MSSQL) 연결 및 설정 관련 라이브러리|Dll|TRA.iTOPS.Diagnostics.dll, TRA.iTOPS.Contracts.dll|완료|
   |TRA.iTOPS.Biz.dll|화면별 DB쿼리를 모아놓은 라이브러리|Dll|TRA.iTOPS.Service.Core, TRA.iTOPS.Contracts|완료|
   |TRA.iTOPS.Windows.Set.dll|로그인 및 메인화면 및 화면Style 라이브러리|Dll|TRA.iTOPS.Contracts, TRA.iTOPS.Biz|완료|
   |TRA.iTOPS.Win.dll|업무화면 라이브러리|Dll|TRA.iTOPS.Windows.Set,TRA.iTOPS.Contracts,TRA.iTOPS.Biz|완료|
   |TRA.iTOP.MFCDLL.dll|업무 화면별 프린트 및 EasyPayNumber생성 관련 MFC 라이브러리|Dll|TRA.iTOPS.Win|완료|

# 5. Appendix - Reference
   - South Africa Traffic Law Office Book
     - [TRAFFIC LAW OFFENCE BOOK 2014 FINAL.pdf](https://github.com/MinkiPaPa/04.Traffic-Enforcement-System-Client/files/7851510/TRAFFIC.LAW.OFFENCE.BOOK.2014.FINAL.pdf)
