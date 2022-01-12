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
   |CODE_GROUP|코드 그룹 분류,관리|완료|
   |CODE_MASTER|그룹화된 세부 코드 분류,관리|완료|
   |PERSONS|단속차량 인적 정보|완료|
   |USERS|교통경찰, 시경 접속 사용자 정보|완료|
   |REGULATIONS|단속 정보 데이터|완료|
   |OFFENCES|단속 정보 검수 데이터|완료|
   |VEHICLE_MAKE|단속 차량 메이커 정보|완료|
   |VEHICLE_TYPE|단속 차량 타잎 정보|완료|
   |OFFENCE_CODE|단속 위반 코드 관리|완료|
   |LOCATION_CODE|단속 도로 및 위치 코드 관리|완료|
   |LOCATION_MAP|단독 도로,위치와 위반코드 맵핑 데이터|완료|
   |CONTRAVENTIONS|단속 위반 데이터|완료|

# 4. Module Architect
   |Module|Content|Type|Derendence|Status|
   |---|---|---|---|---|
   |iTopsMain|Main Program|exe|iTopsLib|완료|
   |iTopsUpRGLTN|단속 영상에서 Plate 추출 및 판독, 업로드|exe|iTopsLib, UseLibALPR, UseLibLTI|완료|
   |iTopsDistribute|검수 담당자 지정|exe|iTopsLib|완료|
   |iTopsInspection|단속 내용 검수|exe|iTopsLib|완료|
   |iTopsMngOffenceCd|Offence Code 관리|exe|iTopsLib|완료|
   |iTopsMngLocationMap|Location와 Offence Code Mapping|exe|iTopsLib|완료|
   |iTopsCdMaster|Code Master 관리|exe|iTopsLib|예정|
   |iTopsUser|사용자 관리|exe|iTopsLib|완료|
   |iTopsMngLocationCd|Location Code 관리|exe|iTopsLib|완료|
   |iTopsLib|iTops 공통 라이브러리|dll|iTopsFTP, DBLibUpRGLTN, DBLibMngOffenceCd, DBCodes DBLibMngLocationMap, DBLibInspection, DBLibDistribute|완료|
   |iTopsFTP|Ftp 라이브러리|dll|N/A|완료|
   |AlprNet|OpenALPR 외부 라이브러리를 활용한 번호판 판독|dll|N/A|외부 라이브러리 활용, 완료|
   |VehicleClassifierNet|OpenALPR 외부 라이브러리를 활용한 차량 정보 추출|dll|N/A|외부 라이브러리 활용, 완료|
   |UseLibALPR|Alpr SDK 제어|dll|AlprNet, VehicleClassifierNet|완료|
   |UseLibLTI|LTI 장비의 단속 Data 추출 SDK활용|dll|N/A|완료|
   |LibMngOffenceCd|iTopsMngOffenceCd.exe의 Data 처리 라이브러리|dll|N/A|완료|
   |DBLibMngLocationMap|iTopsMngLocationMap.exe의 Data 처리 라이브러리|dll|N/A|완료|
   |DBLibInspection|iTopsInspection.exe의 Data 처리 라이브러리|dll|N/A|완료|
   |DBLibDistribute|iTopsDistribute.exe 의 Data 처리 라이브러리|dll|N/A|완료|
   |DBCodes|Code Master 및 각종 코드 테이블 조회 라이브러리|dll|N/A|완료|

# 5. Appendix - Reference
   - South Africa Traffic Law Office Book
     - [TRAFFIC LAW OFFENCE BOOK 2014 FINAL.pdf](https://github.com/MinkiPaPa/04.Traffic-Enforcement-System-Client/files/7851510/TRAFFIC.LAW.OFFENCE.BOOK.2014.FINAL.pdf)
