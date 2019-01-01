select (select title from Programme where id = prog2) title, avg(s) from(
    select prog1, prog2, avg(similarity) s from(
        select distinct p1.id prog1, p2.id prog2, f.type feats, 
        (
            WITH 
            count_and AS (SELECT round(count(1), 4) cand  from (
                select id from feature where (
                    id in (select feature_id from FeatureExample where programme_id = p1.id) and
                    id in (select feature_id from FeatureExample where programme_id = p2.id)) and 
                    type like f.type
                )
            ),
            count_or AS (SELECT round(count(1), 4) cor from (
                select id from feature where (
                    id in (select feature_id from FeatureExample where programme_id = p1.id) or
                    id in (select feature_id from FeatureExample where programme_id = p2.id)) and 
                    type like f.type
                )
            )
            select IFNULL( (cand / cor), 0) dice from count_or, count_and
        ) similarity 
        from Programme p1, Programme p2, Feature f where f.type in (4,7,8)
        and p1.id in (select programme_id from Rating where user_login like 'Przemek' and rating_value = 1) 
        and p2.id not in (select programme_id from Rating where user_login like 'Przemek' and rating_value = 1)
        union
        select distinct p1.id prog1, p2.id prog2, f.type feats,
        (
            WITH
            y1 AS (select IFNULL(value, strftime('%Y', 'now')) year1 from feature where
                id in (select feature_id from FeatureExample where programme_id = p1.id) and
                type like 'date'),
            y2 AS (select IFNULL(value, strftime('%Y', 'now')) year2 from feature where
                id in (select feature_id from FeatureExample where programme_id = p2.id) and
                type like 'date')
            select (1 / (0.05 * abs(year1 - year2) +1)) from y1, y2
        ) similarity
        from Programme p1, Programme p2, Feature f where f.type =2
        and p1.id in (111, 116) and p2.id not in (111, 116)
        union
        select distinct p1.id prog1, p2.id prog2, f.type feats, CASE
            WHEN (select feature_id from FeatureExample where programme_id = p1.id) = (select feature_id from FeatureExample where programme_id = p2.id) 
            THEN 1
            ELSE 0
        END similarity
        from Programme p1, Programme p2, Feature f where f.type in (1,5)
        and p1.id in (select programme_id from Rating where user_login like 'Przemek' and rating_value = 1) 
        and p2.id not in (select programme_id from Rating where user_login like 'Przemek' and rating_value = 1)
    )
    group by prog1, prog2
)
group by prog2
order by avg(s) desc
;
